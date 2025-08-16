using System;

namespace PracticingDDD.Logic
{
    public class SnackPile : ValueObject<SnackPile>
    {
        public Snack Snack { get; }
        public int Quantity { get; }
        public decimal Price { get;  }

        private SnackPile()
        {
            
        }
        public SnackPile(Snack snack, int quantity, decimal price) : this()
        {
            if (quantity < 0 || price < 0 || price % 0.01m > 0)
                throw new InvalidOperationException();
            // if (snack == null)
            //     throw new ArgumentNullException(nameof(snack), "Snack cannot be null");
            Snack = snack;
            Quantity = quantity;
            Price = price;
        }
        public SnackPile SubtractOne()
        {
            // Ensure that quantity is greater than zero before subtracting
            if (Quantity <= 0)
                throw new InvalidOperationException("Cannot subtract from an empty snack pile");
            return new SnackPile(Snack, Quantity - 1, Price);
        }
        protected override bool EqualsCore(SnackPile other)
        {
            return Snack == other.Snack 
                   && Quantity == other.Quantity 
                   && Price == other.Price;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
               
                int hashCode = Snack.GetHashCode();
                hashCode = (hashCode * 397) ^ Quantity;
                hashCode = (hashCode * 397) ^ Price.GetHashCode();
                return hashCode;
            }
        }
    }
}