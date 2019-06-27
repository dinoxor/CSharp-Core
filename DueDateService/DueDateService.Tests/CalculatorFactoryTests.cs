using DueDateService.Calculator;
using System;
using Xunit;

namespace DueDateService.Tests
{
    public class CalculatorFactoryTests
    {
        private readonly CalculatorFactory _calculatorFactory;

        public CalculatorFactoryTests()
        {
            _calculatorFactory = new CalculatorFactory();
        }

        [Fact]
        public void GetCalculator_MonthlyFrequency_ShouldReturnCorrectICalculator()
        {
            var result = _calculatorFactory.GetCalculator("monthly");

            Assert.IsType<MonthlyCalculator>(result);
        }

        [Fact]
        public void GetCalculator_BiWeeklyFrequency_ShouldReturnCorrectICalculator()
        {
            var result = _calculatorFactory.GetCalculator("biweekly");

            Assert.IsType<BiWeeklyCalculator>(result);
        }

        [Fact]
        public void GetCalculator_SemiMonthlyFrequency_ShouldReturnCorrectICalculator()
        {
            var result = _calculatorFactory.GetCalculator("semiMonthly");

            Assert.IsType<SemiMonthlyCalculator>(result);
        }

        [Theory]
        [InlineData("bi-weekly", typeof(BiWeeklyCalculator))]
        [InlineData("semi-monthly", typeof(SemiMonthlyCalculator))]
        [InlineData("month-ly", typeof(MonthlyCalculator))]
        public void GetCalculator_HyphenFrequency_ShouldReturnCorrectCalculator(string frequency, Type calculatorType)
        {
            var result = _calculatorFactory.GetCalculator(frequency);

            Assert.IsType(calculatorType, result);
        }

        [Theory]
        [InlineData("BIWEEKLY", typeof(BiWeeklyCalculator))]
        [InlineData("Bi-weekly", typeof(BiWeeklyCalculator))]
        [InlineData("SEMIMONTHLY", typeof(SemiMonthlyCalculator))]
        [InlineData("Semi-Monthly", typeof(SemiMonthlyCalculator))]
        [InlineData("MONTHLY", typeof(MonthlyCalculator))]
        [InlineData("Monthly", typeof(MonthlyCalculator))]
        public void GetCalculator_CapsFrequency_ShouldReturnCorrectCalculator(string frequency, Type calculatorType)
        {
            var result = _calculatorFactory.GetCalculator(frequency);

            Assert.IsType(calculatorType, result);
        }

        [Theory]
        [InlineData("", "Invalid frequency.")]
        //[InlineData(string.Empty)] //not valid for XUnit
        [InlineData(null, "Null frequency not allowed")]
        [InlineData("something","Invalid frequency.")]
        [InlineData("1234","Invalid frequency.")]
        public void GetCalculator_InvalidFrequency_ShouldThrowException(string invalidFrequency, string expectedExceptionMessage)
        {
            var exception = Assert.Throws<Exception>(() => _calculatorFactory.GetCalculator(invalidFrequency));
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }

        //[Theory]
        //[InlineData("","something","1234","  ")]
        //public void GetCalculator_InvalidFrequency2_ShouldThrowException(string[] invalidFrequencies)
        //{
        //    var exception = Assert.Throws<Exception>(() => _calculatorFactory.GetCalculator(invalidFrequencies));
        //}
    }
}
