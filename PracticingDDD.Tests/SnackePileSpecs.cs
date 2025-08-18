using System;
using FluentAssertions;
using PracticingDDD.Logic;
using Xunit;

namespace PracticingDDD.Tests
{
    public class SnackePileSpecs
    {
        [Fact]
        public void Constructor_Should_Throw_ArgumentNullException_When_Snack_Is_Null()
        {
            // Arrange
            Snack snack = null;
            int quantity = 5;
            decimal price = 1.50m;

            // Act & Assert
            Action action = () => new SnackPile(snack, quantity, price);
            action.Should().Throw<ArgumentNullException>()
                .WithParameterName("snack")
                .WithMessage("Snack cannot be null*");
        }
        
        [Fact]
        public void Constructor_Should_Throw_InvalidOperationException_When_Quantity_Is_Negative()
        {
            // Arrange
            int quantity = -1;
            decimal price = 1.50m;

            // Act & Assert
            Action action = () => new SnackPile(Snack.Chochlate, quantity, price);
            action.Should().Throw<InvalidOperationException>();
        }
        [Fact]
        public void Constructor_Should_Throw_InvalidOperationException_When_Price_Is_Negative()
        {
            // Arrange
            int quantity = 5;
            decimal price = -1.50m;

            // Act & Assert
            Action action = () => new SnackPile(Snack.Chochlate, quantity, price);
            action.Should().Throw<InvalidOperationException>();

        }
        [Fact]
        public void Constructor_Should_Throw_InvalidOperationException_When_Price_Has_More_Than_Two_Decimal_Places()
        {
            // Arrange
            int quantity = 5;
            decimal price = 1.555m; // More than 2 decimal places

            // Act & Assert
            Action action = () => new SnackPile(Snack.Chochlate, quantity, price);
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Constructor_Should_Not_Throw_When_Price_Has_Valid_Decimal_Places()
        {
            // Arrange
            int quantity = 5;
            decimal price = 1.50m; // Valid 2 decimal places

            // Act & Assert
            Action action = () => new SnackPile(Snack.Chochlate, quantity, price);
            action.Should().NotThrow();
        }
        
    }
}