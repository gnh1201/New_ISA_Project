using System;
using System.Collections.Generic;

namespace ISA_Agent
{
    public class LicenseModel
    {
        public int? ID { get; set; }
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ProductName { get; set; }
        public int? RenewPeriod { get; set; }
        public string Content { get; set; }
        public DateTime ExpireDate { get; set; }
        public int? FileID { get; set; }
    }

    public class LicenseAjaxModel
    {
        public IEnumerable<LicenseModel> Data { get; set; }
    }
}
