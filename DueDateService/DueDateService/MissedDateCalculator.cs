using System.Runtime.CompilerServices;
using DueDateService.Model;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: InternalsVisibleTo("DueDateService.Tests")]
namespace DueDateService
{
    public class MissedDateCalculator
    {
        public DueDateResponse Calculate(DueDateRequest request)
        {
            //validation service
            //for now validate everything

            var calculator = new CalculatorFactory().GetCalculator(request.Frequency);

            return calculator.Calculate(request);
            
        }
    }
}
