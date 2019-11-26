using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISA_Agent
{
    class AssignService
    {
        internal static async Task<AssignAjaxModel> GetDataAsync(string asNumber)
        {
            List<AssignModel> assigns = new List<AssignModel>();

            

            // get assign information
            // todo

            return new AssignAjaxModel { Data = assigns };
        }

        public static string GetAppDataFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }

        public static string GetFilePath(string filename)
        {
            return Path.Combine(GetAppDataFolder(), filename);
        }
    }
}
