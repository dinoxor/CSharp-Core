using System;
using System.Collections.Generic;
using DueDateService.Model;

namespace DueDateService.Calculator
{
    internal class SemiMonthlyCalculator : ICalculator
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

        private List<string> GetMissedDates(DueDateRequest request, int numberOfMissedPayments)
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
    }
}
