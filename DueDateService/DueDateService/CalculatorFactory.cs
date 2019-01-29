using DueDateService.Calculator;
using System;
using System.Collections.Generic;
using System.Text;

namespace DueDateService
{
    internal class CalculatorFactory
    {
        public ICalculator GetCalculator(string frequency)
        {
            var frequencyWithoutHyphen = frequency.Replace("-", string.Empty);

            if (string.Equals(frequency, "monthly", StringComparison.OrdinalIgnoreCase))
            {
                return new MonthlyCalculator();
            }
            if (string.Equals(frequency, "semimonthly", StringComparison.OrdinalIgnoreCase))
            {
                return new SemiMonthlyCalculator();
            }
            if (string.Equals(frequencyWithoutHyphen, "biweekly", StringComparison.OrdinalIgnoreCase))
            {
                return new BiWeeklyCalculator();
            }

            throw new Exception("Invalid frequency.");
        }


    }
}
