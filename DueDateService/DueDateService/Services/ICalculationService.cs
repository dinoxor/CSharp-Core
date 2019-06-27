using System.Collections.Generic;
using DueDateService.Model;

namespace DueDateService.Services
{
    internal interface ICalculationService
    {
        int CalculateNumberOfMissedDates_Monthly(DueDateRequest request);
        int CalculateNumberOfMissedPayments_BiWeekly(DueDateRequest request);
        int CalculateNumberOfMissedPayments_SemiMonthly(DueDateRequest request);
        List<string> GetMissedDates_BiWeekly(DueDateRequest request, int numberOfMissedPayments);
        List<string> GetMissedDates_Monthly(DueDateRequest request, int numberOfMissedDates);
        List<string> GetMissedDates_SemiMonthly(DueDateRequest request, int numberOfMissedPayments);
    }
}