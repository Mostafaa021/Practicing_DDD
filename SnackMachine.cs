
using System;
using System.Linq;
using static PracticingDDD.Logic.Money;

namespace PracticingDDD.Logic
{
    public class SnackMachine : Entity
    {

        public virtual Money MoneyInside { get; protected set; } = None;
        public virtual Money MoneyInTransaction { get; protected set; } = None;


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
        public virtual void BuySnack()
        {
            MoneyInside += MoneyInTransaction;

            MoneyInTransaction = None; // Reset transaction money to None
        }
        
    }


}