using System;
using FluentAssertions;
using PracticingDDD.Logic;
using Xunit;

namespace PracticingDDD.Tests
{
    public class MoneySpecs
    {
        [Fact]
        // write unit test that sum two Money objects 
        public void Sum_of_two_money_objects_produces_correct_result()
        {
            // Arrange
            var money1 = new Money(1, 2, 3, 4, 5, 6);
            var money2 = new Money(1, 2, 3, 4, 5, 6);

            // Act
            var sum = money1 + money2;

            // Assert
            sum.OneCentCount.Should().Be(2);
            sum.TenCentCount.Should().Be(4);
            sum.QuarterCount.Should().Be(6);
            sum.OneDollarCount.Should().Be(8);
            sum.FiveDollarCount.Should().Be(10);
            sum.TwentyDollarCount.Should().Be(12);
        }
        [Fact]
        public void Subtraction_of_two_moneys_produces_correct_result()
        {
            Money money1 = new Money(10, 10, 10, 10, 10, 10);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);
        
            Money result = money1 - money2;
        
            result.OneCentCount.Should().Be(9);
            result.TenCentCount.Should().Be(8);
            result.QuarterCount.Should().Be(7);
            result.OneDollarCount.Should().Be(6);
            result.FiveDollarCount.Should().Be(5);
            result.TwentyDollarCount.Should().Be(4);
        }
        [Fact]
        public void SubtractionOperator_WhenSubtractingSpecificDenominationExcess_ThrowsInvalidOperationException()
        {
            // Arrange - same total amount but one denomination exceeds available
            var money1 = new Money(5, 0, 0, 0, 0, 0); // 5 cents
            var money2 = new Money(3, 1, 0, 0, 0, 0); // 3 cents + 1 dime = 13 cents

            // Act & Assert
            Action action = () =>
            {
                var money = money1 - money2;
            };
            action.Should().Throw<InvalidOperationException>()
                .WithMessage("Cannot subtract more money than available");
        }
        
        [Fact]
        public void Two_money_instances_equal_if_contain_the_same_money_amount()
        {
            Money money1 = new Money(1, 2, 3, 4, 5, 6);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            money1.Should().Be(money2);
            money1.GetHashCode().Should().Be(money2.GetHashCode());
        }
        [Fact]
        public void Two_money_instances_do_not_equal_if_contain_different_money_amounts()
        {
            Money dollar = new Money(0, 0, 0, 1, 0, 0);
            Money hundredCents = new Money(100, 0, 0, 0, 0, 0);

            dollar.Should().NotBe(hundredCents);
            dollar.GetHashCode().Should().NotBe(hundredCents.GetHashCode());
        }
        [Theory]
        [InlineData(-1, 0, 0, 0, 0, 0)]
        [InlineData(0, -2, 0, 0, 0, 0)]
        [InlineData(0, 0, -3, 0, 0, 0)]
        [InlineData(0, 0, 0, -4, 0, 0)]
        [InlineData(0, 0, 0, 0, -5, 0)]
        [InlineData(0, 0, 0, 0, 0, -6)]
        public void Cannot_create_money_with_negative_value(
            int oneCentCount,
            int tenCentCount,
            int quarterCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount)
        {
            Action action = () => new Money(
                oneCentCount,
                tenCentCount,
                quarterCount,
                oneDollarCount,
                fiveDollarCount,
                twentyDollarCount);

            action.Should().Throw<InvalidOperationException>();
        }
            [Theory]
            [InlineData(0, 0, 0, 0, 0, 0, 0)]
            [InlineData(1, 0, 0, 0, 0, 0, 0.01)]
            [InlineData(1, 2, 0, 0, 0, 0, 0.21)]
            [InlineData(1, 2, 3, 0, 0, 0, 0.96)]
            [InlineData(1, 2, 3, 4, 0, 0, 4.96)]
            [InlineData(1, 2, 3, 4, 5, 0, 29.96)]
            [InlineData(1, 2, 3, 4, 5, 6, 149.96)]
            [InlineData(11, 0, 0, 0, 0, 0, 0.11)]
            [InlineData(110, 0, 0, 0, 100, 0, 501.1)]
            public void Amount_is_calculated_correctly(
                int oneCentCount,
                int tenCentCount,
                int quarterCount,
                int oneDollarCount,
                int fiveDollarCount,
                int twentyDollarCount,
                double expectedAmount)
            {
                Money money = new Money(
                    oneCentCount,
                    tenCentCount,
                    quarterCount,
                    oneDollarCount,
                    fiveDollarCount,
                    twentyDollarCount);
                
                money.Amount.Should().Be((decimal)expectedAmount);
            }
            [Theory]
            [InlineData(1, 0, 0, 0, 0, 0, "¢1")]
            [InlineData(0, 0, 0, 1, 0, 0, "$1.00")]
            [InlineData(1, 0, 0, 1, 0, 0, "$1.01")]
            [InlineData(0, 0, 2, 1, 0, 0, "$1.50")]
            public void To_string_returns_correct_string_representation(
                int oneCentCount,
                int tenCentCount,
                int quarterCount,
                int oneDollarCount,
                int fiveDollarCount,
                int twentyDollarCount,
                string expectedString)
            {
                Money money = new Money(
                    oneCentCount,
                    tenCentCount,
                    quarterCount,
                    oneDollarCount,
                    fiveDollarCount,
                    twentyDollarCount);

                money.ToString().Should().Be(expectedString);
            }

    }
}