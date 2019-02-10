using DueDateService.Calculator;
using DueDateService.Services;
using System;

namespace DueDateService
{
    internal class CalculatorFactory
    {
        public ICalculator GetCalculator(string frequency)
        {
            string frequencyWithoutHyphen;

            try
            {
                frequencyWithoutHyphen = frequency.Replace("-", string.Empty);
            }
            catch (NullReferenceException ex)
            {
                throw new Exception("Null frequency not allowed");
            }            

            if (string.Equals(frequencyWithoutHyphen, "monthly", StringComparison.OrdinalIgnoreCase))
            {
                return new MonthlyCalculator(new CalculationService());
            }
            if (string.Equals(frequencyWithoutHyphen, "semimonthly", StringComparison.OrdinalIgnoreCase))
            {
                return new SemiMonthlyCalculator(new CalculationService());
            }
            if (string.Equals(frequencyWithoutHyphen, "biweekly", StringComparison.OrdinalIgnoreCase))
            {
                return new BiWeeklyCalculator(new CalculationService());
            }

            throw new Exception("Invalid frequency.");
        }
    }
}
