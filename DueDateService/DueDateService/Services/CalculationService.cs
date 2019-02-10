using DueDateService.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DueDateService.Services
{
    internal class CalculationService : ICalculationService
    {
        #region Monthly
        public int CalculateNumberOfMissedDates_Monthly(DueDateRequest request)
        {
            int monthsDifference = request.CurrentDate.Month - request.DueDate.Month;

            var yearDifferenceInMonths = (request.CurrentDate.Year - request.DueDate.Year) * 12;

            if (request.CurrentDate.Day <= request.DueDate.Day)
            {
                monthsDifference--;
            }

            return monthsDifference + yearDifferenceInMonths;
        }

        public  List<string> GetMissedDates_Monthly(DueDateRequest request, int numberOfMissedDates)
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

        #endregion Monthly

        #region BiWeekly
        public int CalculateNumberOfMissedPayments_BiWeekly(DueDateRequest request)
        {
            var totalDays = (request.CurrentDate - request.DueDate).TotalDays;

            var numberOfMissedPayments = Math.Ceiling(totalDays / 14) - 1;

            return Convert.ToInt32(numberOfMissedPayments);
        }

        public List<string> GetMissedDates_BiWeekly(DueDateRequest request, int numberOfMissedPayments)
        {
            var missedDueDates = new List<string>();

            for (int i = 1; i <= numberOfMissedPayments; i++)
            {
                missedDueDates.Add(request.DueDate.AddDays(i * 14).ToShortDateString());
            }

            return missedDueDates;
        }

        public int CalculateNumberOfMissedPayments_SemiMonthly(DueDateRequest request)
        {
            var numberOfMissedPayments = (request.CurrentDate.Month - request.DueDate.Month - 1) * 2;

            if (request.DueDate.Day == request.DueDay1)
            {
                numberOfMissedPayments++;
            }

            if (request.CurrentDate.Day > request.DueDay1)
            {
                numberOfMissedPayments++;
            }

            if (request.CurrentDate.Day > request.DueDay2)
            {
                numberOfMissedPayments++;
            }

            return numberOfMissedPayments;
        }
        #endregion BiWeekly

        #region SemiMonthly
        public List<string> GetMissedDates_SemiMonthly(DueDateRequest request, int numberOfMissedPayments)
        {
            List<string> missedDates = new List<string>();

            if (request.DueDate.Day == request.DueDay1)
            {
                //same month with day2
                if (request.DueDay2 == 31)
                {
                    missedDates.Add(new DateTime(request.DueDate.Year, request.DueDate.Month, DateTime.DaysInMonth(request.DueDate.Year, request.DueDate.Month)).ToShortDateString());
                }
                else
                {
                    //handle wierd cases for February if dueday2 = 30 or 29
                    try
                    {
                        missedDates.Add(new DateTime(request.DueDate.Year, request.DueDate.Month, request.DueDay2).ToShortDateString());
                    }
                    catch
                    {
                        missedDates.Add(new DateTime(request.DueDate.Year, request.DueDate.Month, DateTime.DaysInMonth(request.DueDate.Year, request.DueDate.Month)).ToShortDateString());
                    }
                }
                numberOfMissedPayments--;
            }

            int addMonth = 1;
            bool useDay1 = true;

            while (numberOfMissedPayments > 0)
            {
                var tempDate = request.DueDate.AddMonths(addMonth);

                if (useDay1)
                {
                    missedDates.Add(new DateTime(tempDate.Year, tempDate.Month, request.DueDay1).ToShortDateString());

                    useDay1 = false;
                }
                else
                {
                    if (request.DueDay2 == 31)
                    {
                        missedDates.Add(new DateTime(tempDate.Year, tempDate.Month, DateTime.DaysInMonth(tempDate.Year, tempDate.Month)).ToShortDateString());
                    }
                    else
                    {
                        missedDates.Add(new DateTime(tempDate.Year, tempDate.Month, request.DueDay2).ToShortDateString());
                    }

                    addMonth++;
                    useDay1 = true;
                }

                numberOfMissedPayments--;
            }

            return missedDates;
        }
        #endregion SemiMonthly
    }
}
