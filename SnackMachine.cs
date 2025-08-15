
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static PracticingDDD.Logic.Money;

namespace PracticingDDD.Logic
{
    public class SnackMachine : AggregateRoot
    {

        public virtual Money MoneyInside { get; protected set; } 
        public virtual Money MoneyInTransaction { get; protected set; }

        protected virtual IList<Slot> Slots { get; set; } 

        public SnackMachine()
        {
            MoneyInside = None; 
            MoneyInTransaction = None;
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
            MoneyInTransaction += money;

        }
        public virtual void ReturnMoney()
        {
           MoneyInTransaction = None; // Reset transaction money to None
        }
        public virtual void BuySnack(int position)
        {
            var slot = GetSlot(position);
            slot.SnackPile =  slot.SnackPile.SubtractOne();
            
            MoneyInside += MoneyInTransaction;
            MoneyInTransaction = None; // Reset transaction money to None
        }
        public virtual void LoadSnacks(int position, SnackPile snackPile)
        {
            var slot = GetSlot(position);
            slot.SnackPile = snackPile;
        }
        
    }


}