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
            List<DeviceModel> devices = new List<DeviceModel>();

            // get assign information


            // get device information
            devices.Add(new DeviceModel
            {
                IP = DeviceService.GetPrimaryIP(),
                MAC = DeviceService.GetPrimaryMAC(),
                Version = DeviceService.GetOSVersion(),
                Name = DeviceService.GetComputerName()
            });

            return new AssignAjaxModel
            {
                AssignData = assigns,
                DeviceData = devices
            };
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
