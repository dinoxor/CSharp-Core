using System.Collections.Generic;
using DueDateService.Model;
using DueDateService.Services;

namespace DueDateService.Calculator
{
    internal class SemiMonthlyCalculator : ICalculator
    {
        private readonly ICalculationService _calculationService;

        public SemiMonthlyCalculator(ICalculationService calculationService)
        {
            calculationService = _calculationService;
        }

        public DueDateResponse Calculate(DueDateRequest request)
        {
            var numberOfMissedPayments = _calculationService.CalculateNumberOfMissedPayments_SemiMonthly(request);

            //include first due date
            numberOfMissedPayments++;
            var missedDueDates = new List<string>()
            {
                { request.DueDate.ToShortDateString() }
            };

            missedDueDates.AddRange(_calculationService.GetMissedDates_SemiMonthly(request, numberOfMissedPayments));

            return new DueDateResponse
            {
                NumberOfMissedPayments = numberOfMissedPayments,
                MissedDates = missedDueDates
            };
        }        
    }
}
