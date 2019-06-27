using DueDateService.Model;
using DueDateService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DueDateService.Tests.Services
{
    public class CalculationServiceTests
    {
        private readonly CalculationService _calculationService;

        public CalculationServiceTests()
        {
            _calculationService = new CalculationService();
        }

        #region Monthly
        [Theory]
        [InlineData("2019-01-01", "2019-01-15", 1)]
        [InlineData("2019-01-01", "2019-02-01", 1)]
        [InlineData("2019-01-01", "2019-02-02", 2)]
        [InlineData("2019-01-01", "2019-02-15", 2)]
        [InlineData("2019-01-01", "2019-03-01", 2)]
        [InlineData("2019-01-01", "2019-03-02", 3)]
        public void CalculateNumberOfMissedDates_Monthly_ValidEdgeCases_ShouldReturnCorrectNumberOfMissedDates(string dueDate, string currentDate, int expectedResult)
        {
            var request = new DueDateRequest
            {
                DueDate = DateTime.Parse(dueDate),
                CurrentDate = DateTime.Parse(currentDate)
            };

            var actualResult = _calculationService.CalculateNumberOfMissedDates_Monthly(request);

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("2018-11-01", "2019-01-02", 3)]
        [InlineData("2018-12-01", "2019-02-02", 3)]
        [InlineData("2019-12-01", "2021-02-02", 15)]
        public void CalculateNumberOfMissedDates_Monthly_NewYearDates_ShouldReturnCorrectNumberOfMissedDates(string dueDate, string currentDate, int expectedResult)
        {
            var request = new DueDateRequest
            {
                DueDate = DateTime.Parse(dueDate),
                CurrentDate = DateTime.Parse(currentDate)
            };

            var actualResult = _calculationService.CalculateNumberOfMissedDates_Monthly(request);

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("2019-01-01", new string[] { "1/1/2019", "2/1/2019" }, 2)]
        [InlineData("2019-01-01", new string[] { "1/1/2019", "2/1/2019", "3/1/2019" }, 3)]
        public void GetMissedDates_Monthly_ValidDueDate_ShouldReturnValidDates(string dueDate, string[] expectedResults, int numberOfMissedDates)
        {
            var request = new DueDateRequest
            {
                DueDate = DateTime.Parse(dueDate),
            };            

            var actualResults = _calculationService.GetMissedDates_Monthly(request, numberOfMissedDates);

            Assert.Equal(expectedResults, actualResults);
        }

        [Theory]
        [InlineData("2019-01-31", new string[] { "1/31/2019", "2/28/2019" }, 2)]
        [InlineData("2019-02-28", new string[] { "2/28/2019", "3/31/2019" }, 2)]
        [InlineData("2019-01-31", new string[] { "1/31/2019", "2/28/2019", "3/31/2019" }, 3)]
        public void GetMissedDates_Monthly_ValidDueDateOnLastDayOfMonth_ShouldReturnValidDates(string dueDate, string[] expectedResults, int numberOfMissedDates)
        {
            var request = new DueDateRequest
            {
                DueDate = DateTime.Parse(dueDate),
            };

            var actualResults = _calculationService.GetMissedDates_Monthly(request, numberOfMissedDates);

            Assert.Equal(expectedResults, actualResults);
        }
        #endregion Monthly

        #region BiWeekly
        [Theory]
        [InlineData("2019-01-01", "2019-01-14", 1)]
        [InlineData("2019-01-01", "2019-01-15", 1)]
        [InlineData("2019-01-01", "2019-01-16", 2)]
        [InlineData("2019-01-01", "2019-01-29", 2)]
        [InlineData("2019-01-01", "2019-01-30", 3)]
        [InlineData("2018-12-25", "2019-01-08", 1)]
        [InlineData("2018-12-25", "2019-01-09", 2)]
        [InlineData("2018-12-25", "2019-01-23", 3)]
        public void CalculateNumberOfMissedPayments_BiWeekly_ValidEdgeCases_ShouldReturnCorrectNumberOfMissedDates(string dueDate, string currentDate, int expectedResult)
        {
            var request = new DueDateRequest
            {
                DueDate = DateTime.Parse(dueDate),
                CurrentDate = DateTime.Parse(currentDate)
            };

            var actualResult = _calculationService.CalculateNumberOfMissedPayments_BiWeekly(request);

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("2019-01-01", new string[] { "1/1/2019", "1/15/2019" }, 2)]
        [InlineData("2019-01-01", new string[] { "1/1/2019", "1/15/2019", "1/29/2019" }, 3)]
        [InlineData("2019-01-01", new string[] { "1/1/2019", "1/15/2019", "1/29/2019", "2/12/2019" }, 4)]
        [InlineData("2019-01-01", new string[] { "1/1/2019", "1/15/2019", "1/29/2019", "2/12/2019", "2/26/2019" }, 5)]
        public void GetMissedDates_BiWeekly_ValidDueDate_ShouldReturnValidDates(string dueDate, string[] expectedResults, int numberOfMissedDates)
        {
            var request = new DueDateRequest
            {
                DueDate = DateTime.Parse(dueDate)
            };

            var actualResults = _calculationService.GetMissedDates_BiWeekly(request, numberOfMissedDates);

            Assert.Equal(expectedResults, actualResults);
        }
        #endregion BiWeekly

        #region SemiMonthly

        #endregion SemiMonthly


    }
}
