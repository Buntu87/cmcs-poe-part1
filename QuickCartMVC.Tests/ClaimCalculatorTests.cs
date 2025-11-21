using Xunit;
using QuickCartMVC.Services;
using System;

namespace QuickCartMVC.Tests
{
    public class ClaimCalculatorTests
    {
        private readonly ClaimCalculator _calc = new ClaimCalculator();

        [Theory]
        [InlineData(1.5, 200, 300.00)]
        [InlineData(10, 12.34, 123.40)]
        [InlineData(0.25, 1000, 250.00)]
        public void CalculatePayment_ReturnsExpected(decimal hours, decimal rate, decimal expected)
        {
            var result = _calc.CalculatePayment(hours, rate);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ValidateClaim_RejectsNegativeHours()
        {
            var (isValid, msg) = _calc.ValidateClaim(-1m, 50m);
            Assert.False(isValid);
            Assert.Contains("negative", msg, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void ValidateClaim_RejectsZeroHours()
        {
            var (isValid, msg) = _calc.ValidateClaim(0m, 50m);
            Assert.False(isValid);
            Assert.Contains("greater than zero", msg, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void ValidateClaim_AcceptsNormalValues()
        {
            var (isValid, msg) = _calc.ValidateClaim(10m, 200m);
            Assert.True(isValid);
            Assert.True(string.IsNullOrEmpty(msg));
        }
    }
}
