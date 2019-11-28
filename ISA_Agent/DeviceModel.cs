using System;
using System.Collections.Generic;

namespace ISA_Agent
{
    public class DeviceModel
    {
        public Int32 ID { get; set; }
        public string Status { get; set; }
        public Int32 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public string MAC { get; set; }
        public string Version { get; set; }
        public Int32 AssignID { get; set; }
        public Int32 GroupID { get; set; }
    }

    public class DeviceAjaxModel
    {
        public IEnumerable<DeviceModel> Data { get; set; }
    }
}
