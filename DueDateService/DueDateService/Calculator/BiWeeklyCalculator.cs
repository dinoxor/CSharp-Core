using System;
using System.Collections.Generic;
using DueDateService.Model;

namespace DueDateService.Calculator
{
    internal class BiWeeklyCalculator : ICalculator
    {
        public DueDateResponse Calculate(DueDateRequest request)
        {
            var numberofMissedPayments = CalculateNumberOfMissedPayments(request);

            return new DueDateResponse
            {
                NumberOfMissedPayments = numberofMissedPayments,
                MissedDates = GetMissedDates(request, numberofMissedPayments),
            };

        }

        private int CalculateNumberOfMissedPayments(DueDateRequest request)
        {
            var totalDays = (request.CurrentDate - request.DueDate).TotalDays;

            var numberOfMissedPayments = Math.Ceiling(totalDays / 14) - 1;

            return Convert.ToInt32(numberOfMissedPayments);
        }

        private List<DateTime> GetMissedDates (DueDateRequest request, int numberOfMissedPayments)
        {
            var missedDueDates = new List<DateTime>();

            for (int i = 1; i <= numberOfMissedPayments; i++)
            {
                missedDueDates.Add(request.DueDate.AddDays(i*14));
            }

            return missedDueDates;
        }
    }
}
