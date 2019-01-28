using System;

namespace DueDateService.Model
{
    public class DueDateRequest
    {
        public DateTime DueDate { get; set; }
        public DateTime CurrentDate { get; set; }
        public string Frequency { get; set; }
        public int DueDay1 { get; set; }
        public int DueDay2 { get; set; }
    }
}
