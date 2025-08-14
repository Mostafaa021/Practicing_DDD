
using System;
using System.Linq;
using static PracticingDDD.Logic.Money;

namespace PracticingDDD.Logic
{
    public class SnackMachine : Entity
    {

        public virtual Money MoneyInside { get; protected set; } = None;
        public virtual Money MoneyInTransaction { get; protected set; } = None;


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
        public void BuySnack()
        {
            MoneyInside += MoneyInTransaction;

            MoneyInTransaction = None; // Reset transaction money to None
        }
        
    }


}