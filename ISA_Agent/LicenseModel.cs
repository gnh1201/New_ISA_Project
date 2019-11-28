using System;
using System.Collections.Generic;

namespace ISA_Agent
{
    public class LicenseModel
    {
        public Int32 ID { get; set; }
        public string Status { get; set; }
        public Int32 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string DisplayName { get; set; }
        public string RenewType { get; set; }
        public string Publisher { get; set; }
        public string Memo { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public Int32 FileID { get; set; }
    }

    public class LicenseAjaxModel
    {
        public IEnumerable<LicenseModel> Data { get; set; }
    }
}
