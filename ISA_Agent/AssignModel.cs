using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISA_Agent
{
    public class AssignModel
    {
        public int? ID { get; set; }
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string AssignNumber { get; set; }
        public string BizName { get; set; }
        public string BizNumber { get; set; }
        public string UserName { get; set; }
        public string UserOrganization { get; set; }
        public string UserContact { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public int? GroupID { get; set; }
    }
}
