using DueDateService.Model;
using DueDateService.Services;
using System.Collections.Generic;

namespace DueDateService.Calculator
{
    internal class MonthlyCalculator : ICalculator
    {
        private readonly ICalculationService _calculationService;

        public MonthlyCalculator(ICalculationService calculationService)
        {
            calculationService = _calculationService;
        }

        public DueDateResponse Calculate(DueDateRequest request)
        {
            var numberOfMissedPayments = _calculationService.CalculateNumberOfMissedDates_Monthly(request);

            var missedDueDates = new List<string>()
            {
                { request.DueDate.ToShortDateString() }
            };

            missedDueDates.AddRange(_calculationService.GetMissedDates_Monthly(request, numberOfMissedPayments));

            return new DueDateResponse
            {
                NumberOfMissedPayments = numberOfMissedPayments,
                MissedDates = missedDueDates
            };
        }   
    }
}
