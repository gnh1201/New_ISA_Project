using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmbedIO;
using HttpMultipartParser;
using LiteDB;

namespace ISA_Agent
{
    class LicenseService
    {
        private static LiteDatabase db = new LiteDatabase(@"ISA.db");

        internal static async Task<LicenseAjaxModel> GetDataAsync()
        {
            var licenses = db.GetCollection<LicenseModel>("licenses");
            var results = licenses.FindAll();
            return new LicenseAjaxModel { Data = results };
        }

        private static void AttachFile()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Title = "라이센스 파일 찾기..."
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // openFileDialog1.FileName;

                var form = new AddLicenseForm();
                form.ShowDialog();
                form.Focus();
            }
        }

        internal static async Task<SuccessModel> AddDataAsync()
        {
            Thread th = new Thread(new ThreadStart(AttachFile));
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            th.Join();

            return new SuccessModel { Success = true };
        }
    }
}
