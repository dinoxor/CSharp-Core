using System;
using System.Diagnostics;
using DueDateService;
using DueDateService.Model;

namespace DueDateConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            DueDateRequest request;

            //request = GetMonthlyRequest();
            request = GetSemiMonthlyRequest();

            var dueDateService = new MissedDateCalculator();
            var response = dueDateService.Calculate(request);

            Debug.WriteLine($"Due date:\t\t{request.DueDate.ToString("MM/dd/yyyy")}");
            Debug.WriteLine($"Current date:\t{request.CurrentDate.ToString("MM/dd/yyyy")}");

            DisplayResponse(response);
        }

        private static void DisplayResponse(DueDateResponse response)
        {
            Debug.WriteLine($"Number of missed payments: {response.NumberOfMissedPayments}");

            Debug.WriteLine("Missed Dates:");
            foreach (var missedDate in response.MissedDates)
            {
                Debug.WriteLine($"{missedDate.ToString("MM/dd/yyyy")}");
            }
        }

        private static DueDateRequest GetMonthlyRequest()
        {
            return new DueDateRequest
            {
                DueDate = DateTime.Parse("2019-01-15"),
                CurrentDate = DateTime.Parse("2019-08-16"),
                Frequency = "monthly"
            };            
        }

        private static DueDateRequest GetSemiMonthlyRequest()
        {
            var request = new DueDateRequest
            {
                DueDate = DateTime.Parse("2019-02-15"),
                CurrentDate = DateTime.Parse("2019-07-15"),
                Frequency = "semiMonthly",
                DueDay1 = 15,
                DueDay2 = 30
            };

            Debug.WriteLine($"{request.DueDay1} , {request.DueDay2}");

            return request;
        }
    }
}
