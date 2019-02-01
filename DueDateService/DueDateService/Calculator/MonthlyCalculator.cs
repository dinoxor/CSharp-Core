using DueDateService.Model;
using System;
using System.Collections.Generic;

namespace DueDateService.Calculator
{
    internal class MonthlyCalculator : ICalculator
    {        

        public DueDateResponse Calculate(DueDateRequest request)
        {
            var numberOfMissedPayments = CalculateNumberOfMissedDates(request);

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

        private int CalculateNumberOfMissedDates(DueDateRequest request)
        {
            int monthsDifference = request.CurrentDate.Month - request.DueDate.Month;

            var yearDifferenceInMonths = (request.CurrentDate.Year - request.DueDate.Year) * 12;

            if (request.CurrentDate.Day <= request.DueDate.Day)
            {
                monthsDifference--;
            }

            return monthsDifference + yearDifferenceInMonths;
        }

        private List<string> GetMissedDates(DueDateRequest request, int numberOfMissedDates)
        {
            var missedDueDates = new List<string>();

            //if last day of the month => following due dates should be last day of the month
            if (request.DueDate.Day == DateTime.DaysInMonth(request.DueDate.Year, request.DueDate.Month))
            {
                for (int i = 1; i <= numberOfMissedDates; i++)
                {
                    var nextMonth = request.DueDate.AddMonths(i);

                    missedDueDates.Add(new DateTime(nextMonth.Year, nextMonth.Month, DateTime.DaysInMonth(nextMonth.Year, nextMonth.Month)).ToShortDateString());
                }
            }
            else
            {
                for (int i = 1; i <= numberOfMissedDates; i++)
                {
                    var nextMonth = request.DueDate.AddMonths(i);

                    missedDueDates.Add(new DateTime(nextMonth.Year, nextMonth.Month, request.DueDate.Day).ToShortDateString());
                }
            }            

            return missedDueDates;

        }
    }
}
