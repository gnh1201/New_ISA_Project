using System;

namespace ISA_Agent
{
    public class DeviceModel
    {
        public int? ID { get; set; }
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public string MAC { get; set; }
        public string Version { get; set; }
        public int? AssignID { get; set; }
        public int? GroupID { get; set; }
    }
}
