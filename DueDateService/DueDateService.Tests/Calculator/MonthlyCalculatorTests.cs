using DueDateService.Calculator;
using DueDateService.Services;
using System;
using Xunit;

namespace DueDateService.Tests.Calculator
{
    public class MonthlyCalculatorTests
    {
        private readonly MonthlyCalculator _monthlyCalculator;

        public MonthlyCalculatorTests()
        {
            _monthlyCalculator = new MonthlyCalculator(new CalculationService());
        }

        [Fact]
        public void Calculate_ValidRequest_()
        {

        }

    }
}
