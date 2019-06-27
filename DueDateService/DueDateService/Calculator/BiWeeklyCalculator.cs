using System.Collections.Generic;
using DueDateService.Model;
using DueDateService.Services;

namespace DueDateService.Calculator
{
    internal class BiWeeklyCalculator : ICalculator
    {
        private readonly ICalculationService _calculationService;

        public BiWeeklyCalculator(ICalculationService calculationService)
        {
            calculationService = _calculationService;
        }

        public DueDateResponse Calculate(DueDateRequest request)
        {
            var numberOfMissedPayments = _calculationService.CalculateNumberOfMissedPayments_BiWeekly(request);

            var missedDueDates = new List<string>()
            {
                { request.DueDate.ToShortDateString() }
            };

            missedDueDates.AddRange(_calculationService.GetMissedDates_BiWeekly(request, numberOfMissedPayments));

            return new DueDateResponse
            {
                NumberOfMissedPayments = numberOfMissedPayments,
                MissedDates = missedDueDates
            };
        }

        
    }
}
