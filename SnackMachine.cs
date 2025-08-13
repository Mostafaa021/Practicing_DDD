
using System;
using System.Linq;
using static PracticingDDD.Logic.Money;

namespace PracticingDDD.Logic
{
    public sealed class SnackMachine : Entity
    {

        public Money MoneyInSide { get; private set; } = None;
        public Money MoneyInTransaction { get; private set; } = None;

      
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
            MoneyInSide += MoneyInTransaction;

            MoneyInTransaction = None; // Reset transaction money to None
        }
    }
    
    
}