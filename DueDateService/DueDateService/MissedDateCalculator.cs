using DueDateService.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DueDateService
{
    public class MissedDateCalculator
    {
        public DueDateResponse Calculate(DueDateRequest request)
        {
            //validation service

            var calculator = new CalculatorFactory().GetCalculator(request.Frequency);

            return calculator.Calculate(request);
            
        }
    }
}
