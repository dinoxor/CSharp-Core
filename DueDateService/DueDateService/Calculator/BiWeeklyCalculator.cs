using System;
using System.Collections.Generic;
using DueDateService.Model;

namespace DueDateService.Calculator
{
    internal class BiWeeklyCalculator : ICalculator
    {
        public DueDateResponse Calculate(DueDateRequest request)
        {
            var numberOfMissedPayments = CalculateNumberOfMissedPayments(request);

            //include first due date
            numberOfMissedPayments++;
            var missedDueDates = new List<string>()
            {
                { request.DueDate.ToShortDateString() }
            };

            missedDueDates.AddRange(GetMissedDates(request, numberOfMissedPayments));

            return new DueDateResponse
            {
                NumberOfMissedPayments = numberOfMissedPayments,
                MissedDates = missedDueDates
            };
        }

        private int CalculateNumberOfMissedPayments(DueDateRequest request)
        {
            var totalDays = (request.CurrentDate - request.DueDate).TotalDays;

            var numberOfMissedPayments = Math.Ceiling(totalDays / 14) - 1;

            return Convert.ToInt32(numberOfMissedPayments);
        }

        private List<string> GetMissedDates (DueDateRequest request, int numberOfMissedPayments)
        {
            var missedDueDates = new List<string>();

            for (int i = 1; i <= numberOfMissedPayments; i++)
            {
                missedDueDates.Add(request.DueDate.AddDays(i*14).ToShortDateString());
            }

            return missedDueDates;
        }
    }
}
