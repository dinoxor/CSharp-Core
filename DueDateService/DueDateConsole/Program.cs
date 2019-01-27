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
            var request = new DueDateRequest
            {
                DueDate = DateTime.Parse("2019-01-31"),
                CurrentDate = DateTime.Parse("2019-04-01"),                
                Frequency = "monthly"
            };

            var dueDateService = new MissedDateCalculator();
            var response = dueDateService.Calculate(request);          
            
            
            Debug.WriteLine($"Number of missed payments: {response.NumberOfMissedPayments}");

            Debug.WriteLine("Missed Dates:");
            foreach (var missedDate in response.MissedDates)
            {
                Debug.WriteLine($"{missedDate.ToString("MM/dd/yyyy")}");
            }
        }
    }
}
