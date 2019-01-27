using System;

namespace DueDateService.Model
{
    public class DueDateRequest
    {
        public DateTime DueDate { get; set; }
        public DateTime CurrentDate { get; set; }
        public string Frequency { get; set; }
        public string DueDay1 { get; set; }
        public string DueDay2 { get; set; }
    }
}
