using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DueDateService.Model
{
    public class DueDateResponse
    {
        public int NumberOfMissedPayments { get; set; }
        public List<String> MissedDates { get; set; }
    }
}
