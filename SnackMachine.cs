
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

        public virtual IList<Slot> Slots { get; protected set; } 

        public SnackMachine()
        {
            MoneyInside = None; 
            MoneyInTransaction = None;
            Slots = new List<Slot>()
            {
                new Slot(this, 1, null, 0, 0m),
                new Slot(this, 2, null, 0, 0m),
                new Slot(this, 3, null, 0, 0m),
            };
            
        }


        public void InsertMoney(Money money )
        {
            Money[] coinsAndNotes =
            {
                OneCent, TenCents, QuarterCents, OneDollar, FiveDollars, TwentyDollars
            };
            if (!coinsAndNotes.Contains(money))
                throw new InvalidOperationException("Invalid money type inserted");
            MoneyInTransaction += money;

        }
        public void ReturnMoney()
        {
           MoneyInTransaction = None; // Reset transaction money to None
        }
        public void BuySnack(int position)
        {
            MoneyInside += MoneyInTransaction;

            MoneyInTransaction = None; // Reset transaction money to None
        }
        public void LoadSnacks(int position, Snack snack, int quantity, decimal price)
        {
            var slot = Slots.Single(s => s.Position == position);
            slot.Snack = snack;
            slot.Quantity = quantity;
            slot.Price = price;
        }
        
    }


}