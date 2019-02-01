using Newtonsoft.Json;
using System;

namespace DueDateService.Model
{
    public class DueDateRequest
    {
        [JsonConverter(typeof(DateConverter))]
        public DateTime DueDate { get; set; }
        [JsonConverter(typeof(DateConverter))]
        public DateTime CurrentDate { get; set; }
        public string Frequency { get; set; }
        public int DueDay1 { get; set; }
        public int DueDay2 { get; set; }
    }
}
