using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DueDateApi
{
    public class DueDateQueryRequest
    {
        public string DueDate { get; set; }
        public string CurrentDate { get; set; }
        public string Frequency { get; set; }
        public string DueDay1 { get; set; }
        public string DueDay2 { get; set; }

    }
}
