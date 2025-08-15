namespace PracticingDDD.Logic
{
    public class Slot : Entity
    {
        public virtual  Snack Snack { get; set; }
        public virtual  SnackMachine SnackMachine { get;set; }
        public virtual  int Quantity { get;set; }
        public virtual  int Position { get;set; }
        public virtual  decimal Price { get;set; }

        protected Slot()
        {
                
        }

        public Slot(
            SnackMachine snackMachine,
            int position,
            Snack snack,
            int quantity,
            decimal price
        ) : this()
        {
            Snack = snack;
            SnackMachine = snackMachine;
            Quantity = quantity;
            Position = position;
            Price = price;
        }
    }
}