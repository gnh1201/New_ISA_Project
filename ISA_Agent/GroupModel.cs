using System;

namespace ISA_Agent
{
    public class GroupModel
    {
        public int? ID { get; set; }
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }
}
