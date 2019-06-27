using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace DueDateService.Model
{
    internal class DateConverter : IsoDateTimeConverter
    {
        public DateConverter()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
