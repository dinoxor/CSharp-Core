using System;
using System.Collections.Generic;
using System.Text;
using DueDateService.Model;

namespace DueDateService.Calculator
{
    internal class SemiMonthlyCalculator : ICalculator
    {
        public DueDateResponse Calculate(DueDateRequest request)
        {
            var numberOfMissedPayments = CalculateNumberOfMissedPayments(request);

            return new DueDateResponse
            {
                NumberOfMissedPayments = numberOfMissedPayments,
                MissedDates = GetMissedDates(request, numberOfMissedPayments)
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

        private List<DateTime> GetMissedDates(DueDateRequest request, int numberOfMissedPayments)
        {
            List<DateTime> missedDates = new List<DateTime>();

            if (request.DueDate.Day == request.DueDay1)
            {
                //same month with day2
                if (request.DueDay2 == 31)
                {
                    missedDates.Add(new DateTime(request.DueDate.Year, request.DueDate.Month, DateTime.DaysInMonth(request.DueDate.Year, request.DueDate.Month)));
                }
                else
                {
                    //handle wierd cases for February if dueday2 = 30 or 29
                    try
                    {
                        missedDates.Add(new DateTime(request.DueDate.Year, request.DueDate.Month, request.DueDay2));
                    }
                    catch
                    {
                        missedDates.Add(new DateTime(request.DueDate.Year, request.DueDate.Month, DateTime.DaysInMonth(request.DueDate.Year, request.DueDate.Month)));
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
                    missedDates.Add(new DateTime(tempDate.Year, tempDate.Month, request.DueDay1));

                    useDay1 = false;
                }
                else
                {
                    if (request.DueDay2 == 31)
                    {
                        missedDates.Add(new DateTime(tempDate.Year, tempDate.Month, DateTime.DaysInMonth(tempDate.Year, tempDate.Month)));
                    }
                    else
                    {
                        missedDates.Add(new DateTime(tempDate.Year, tempDate.Month, request.DueDay2));
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
