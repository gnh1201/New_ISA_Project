using System;
using System.Collections.Generic;

namespace ISA_Agent
{
    public class BundleModel
    {
        public int? ID { get; set; }
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Origin { get; set; }
        public string ResourceName { get; set; }
        public string Default { get; set; }
        public string InstallDate { get; set; }
        public string InstallLocation { get; set; }
        public string Publisher { get; set; }
        public string DisplayIcon { get; set; }
        public string DisplayName { get; set; }
        public string DisplayVersion { get; set; }
        public string HelpLink { get; set; }
        public string UninstallString { get; set; }
        public int? GroupID { get; set; }
        public int? AssignID { get; set; }
        public int? DeviceID { get; set; }
    }

    public class BundleAjaxModel
    {
        public IEnumerable<BundleModel> aaData { get; set; }
    }
}
