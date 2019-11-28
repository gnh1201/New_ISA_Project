using System;
using System.Collections.Generic;

namespace ISA_Agent
{
    public class NoticeModel
    {
        public Int32 ID { get; set; }
        public string Status { get; set; }
        public Int32 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Content { get; set; }
        public Int32 GroupID { get; set; }
    }

    public class NoticeAjaxModel
    {
        public IEnumerable<NoticeModel> Data { get; set; }
    }
}
