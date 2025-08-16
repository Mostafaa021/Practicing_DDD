
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static PracticingDDD.Logic.Money;

namespace PracticingDDD.Logic
{
    public class SnackMachine : AggregateRoot
    {

        public virtual Money MoneyInside { get; protected set; }  // hold whole money in the machine
        public virtual decimal MoneyInTransaction { get; protected set; }

        protected virtual IList<Slot> Slots { get; set; } 

        public SnackMachine()
        {
            MoneyInside = None; 
            MoneyInTransaction = 0;
            Slots = new List<Slot>()
            {
                new Slot(this,1 ),
                new Slot(this,2 ),
                new Slot(this,3),
            };
            
        }
        
       public virtual SnackPile GetSnackPile(int position)
        {
           return GetSlot(position).SnackPile;
        }

        private Slot GetSlot(int position)
        {
            return Slots.Single(s => s.Position == position);
        }
        public virtual void InsertMoney(Money money )
        {
            Money[] coinsAndNotes =
            {
                OneCent, TenCents, QuarterCents, OneDollar, FiveDollars, TwentyDollars
            };
            if (!coinsAndNotes.Contains(money))
                throw new InvalidOperationException("Invalid money type inserted");
            MoneyInTransaction += money.Amount;
            MoneyInside += money ;

        }
        public virtual void ReturnMoney()
        {
            var retunrMoney = MoneyInside.Allocate(MoneyInTransaction); 
            MoneyInside -= retunrMoney; // Remove the money from the machine
            MoneyInTransaction = 0; // Reset transaction money to None
        }
        public virtual void BuySnack(int position)
        {
            var slot = GetSlot(position);
            if (slot.SnackPile.Price > MoneyInTransaction) // here we check if the price of the snack is greater than the money in transaction
                throw new InvalidOperationException("Not enough money in transaction to buy the snack");
            slot.SnackPile =  slot.SnackPile.SubtractOne();
            var change  = MoneyInside.Allocate(MoneyInTransaction - slot.SnackPile.Price); // change is the money left after buying the snack
            if (change.Amount < MoneyInTransaction- slot.SnackPile.Price)
                throw new InvalidOperationException("No change available in the machine");
            MoneyInside -= change;
            MoneyInTransaction = 0; // Reset transaction money to None
        }
        public virtual void LoadSnacks(int position, SnackPile snackPile)
        {
            var slot = GetSlot(position);
            slot.SnackPile = snackPile;
        }
        public virtual void LoadMoney(Money money)
        {
            MoneyInside += money;
        }
        
    }


}