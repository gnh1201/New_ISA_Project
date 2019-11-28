using System;
using System.Collections.Generic;

namespace ISA_Agent
{
    public class AssignModel
    {
        public Int32 ID { get; set; }
        public string Status { get; set; }
        public Int32 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string AssignNumber { get; set; }
        public string BizName { get; set; }
        public string BizNumber { get; set; }
        public string UserName { get; set; }
        public string UserOrganization { get; set; }
        public string UserContact { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public Int32 GroupID { get; set; }
    }

    public class AssignAjaxModel
    {
        public bool Success { get; set; }
        public IEnumerable<AssignModel> Data { get; set; }
    }
}
