using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmbedIO;
using EmbedIO.Actions;
using EmbedIO.Files;
using EmbedIO.WebApi;
using ISA_Agent.Properties;
using Swan;
using Swan.Logging;

namespace ISA_Agent
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MyCustomApplicationContext());
        }
    }

    public class MyCustomApplicationContext : ApplicationContext
    {
        private const bool OpenBrowser = true;
        private const bool UseFileCache = true;
        private NotifyIcon trayIcon;

        public MyCustomApplicationContext()
        {
            // Initialize Tray Icon
            trayIcon = new NotifyIcon()
            {
                Icon = Resources.AppIcon,
                ContextMenu = new ContextMenu(new MenuItem[] {
                    new MenuItem("제어판", OpenWebBrowser),
                    new MenuItem("종료", Exit)
            }),
                Visible = true
            };

            // add mouse double click event
            trayIcon.MouseDoubleClick += new MouseEventHandler(OpenWebBrowser);

            // run web server
            RunWebServer();
        }

        void Exit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            trayIcon.Visible = false;

            Application.Exit();
        }

        void OpenWebBrowser(object sender, EventArgs e)
        {
            var url = "http://*:8877";

            using (var ctSource = new CancellationTokenSource())
            {
                Task.WaitAll(ShowBrowserAsync(url.Replace("*", "localhost"), ctSource.Token));
            }
        }

        private static void RunWebServer()
        {
            var url = "http://*:8877";

            using (var ctSource = new CancellationTokenSource())
            {
                Task.WaitAll(
                    RunWebServerAsync(url, ctSource.Token),
                    OpenBrowser ? ShowBrowserAsync(url.Replace("*", "localhost"), ctSource.Token) : Task.CompletedTask,
                    WaitForUserBreakAsync(ctSource.Cancel));
            }

            // Clean up
            "Bye".Info(nameof(Program));
            Terminal.Flush();

            Console.WriteLine("Press any key to exit.");
            WaitForKeypress();
        }

        // Gets the local path of shared files.
        // When debugging, take them directly from source so we can edit and reload.
        // Otherwise, take them from the deployment directory.
        public static string HtmlRootPath
        {
            get
            {
                var assemblyPath = Path.GetDirectoryName(typeof(Program).Assembly.Location);

#if DEBUG
                return Path.Combine(Directory.GetParent(assemblyPath).Parent.FullName, "html");
#else
                return Path.Combine(assemblyPath, "html");
#endif
            }
        }

        // Create and configure our web server.
        private static WebServer CreateWebServer(string url)
        {
#pragma warning disable CA2000 // Call Dispose on object - this is a factory method.
            var server = new WebServer(o => o
                    .WithUrlPrefix(url)
                    .WithMode(HttpListenerMode.EmbedIO))
                .WithLocalSessionManager()
                .WithCors(
                    // Origins, separated by comma without last slash
                    "http://catswords.github.io,http://catswords.re.kr",
                    // Allowed headers
                    "content-type, accept",
                    // Allowed methods
                    "post")
                .WithWebApi("/assign", m => m.WithController<AssignController>())
                .WithWebApi("/notice", m => m.WithController<NoticeController>())
                .WithWebApi("/bundle", m => m.WithController<BundleController>())
                .WithStaticFolder("/", HtmlRootPath, true, m => m
                    .WithContentCaching(UseFileCache)) // Add static files after other modules to avoid conflicts
                .WithModule(new ActionModule("/", HttpVerbs.Any, ctx => ctx.SendDataAsync(new { Message = "Error" })));

            // Listen for state changes.
            server.StateChanged += (s, e) => $"WebServer New State - {e.NewState}".Info();

            return server;
#pragma warning restore CA2000
        }

        // Create and run a web server.
        private static async Task RunWebServerAsync(string url, CancellationToken cancellationToken)
        {
            using (var server = CreateWebServer(url))
            {
                await server.RunAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        // Open the default browser on the web server's home page.
        private static async Task ShowBrowserAsync(string url, CancellationToken cancellationToken)
        {
            // Be sure to run in parallel.
            await Task.Yield();

            // Fire up the browser to show the content!
            using (var browser = new Process())
            {
                browser.StartInfo = new ProcessStartInfo(url)
                {
                    UseShellExecute = true
                };
                browser.Start();
            }
        }

        // Prompt the user to press any key; when a key is next pressed,
        // call the specified action to cancel operations.
        private static async Task WaitForUserBreakAsync(Action cancel)
        {
            // Be sure to run in parallel.
            await Task.Yield();

            "Press any key to stop the web server.".Info(nameof(Program));
            WaitForKeypress();
            "Stopping...".Info(nameof(Program));
            cancel();
        }

        // Clear the console input buffer and wait for a keypress
        private static void WaitForKeypress()
        {
            while (Console.KeyAvailable)
                Console.ReadKey(true);

            Console.ReadKey(true);
        }
    }
}