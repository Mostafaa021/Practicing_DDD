namespace PracticingDDD.Logic
{
    public class Slot : Entity
    {
        
        public virtual SnackPile SnackPile { get; set; } 
        public virtual  SnackMachine SnackMachine { get;set; }
        public virtual  int Position { get;set; }

        protected Slot()
        {
                
        }

        public Slot(
            SnackMachine snackMachine,
            int position
        ) : this()
        {
            SnackMachine = snackMachine;
            Position = position;
            SnackPile = SnackPile.Empty;
        }
    }
}