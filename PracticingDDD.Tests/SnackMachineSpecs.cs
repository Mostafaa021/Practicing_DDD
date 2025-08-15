using System;
using System.Linq;
using FluentAssertions;
using PracticingDDD.Logic;
using Xunit ; 

using static PracticingDDD.Logic.Money;
namespace PracticingDDD.Tests
{
    public class SnackMachineSpecs
    {
        [Fact]
        public void Return_money_empties_money_in_transaction()
        {
            var snackMachine = new SnackMachine();
            snackMachine.InsertMoney(OneDollar); // Insert one dollar
 
            snackMachine.ReturnMoney();
            snackMachine.MoneyInTransaction.Amount.Should().Be(0m);
         
        }
        [Fact] 
        public void Inserted_money_goes_to_money_in_transaction()
        {
            var snackMachine = new SnackMachine();
 
            snackMachine.LoadSnacks(1 , new Snack("Lays") , 10 , 1m); // Load snacks into the machine
            snackMachine.InsertMoney(OneDollar);
 
            snackMachine.MoneyInTransaction.Amount.Should().Be(1.01m);
        }
        [Fact] 
        public void Cannot_insert_more_than_one_coin_or_note_at_a_time()
        {
            var snackMachine = new SnackMachine();
            var twoCents = OneCent + OneCent; // Two cents
 
            Action action = () => snackMachine.InsertMoney(twoCents);
 
            action.Should().Throw<InvalidOperationException>();
        }
        [Fact]
        public void Money_in_transaction_goes_to_money_inside_after_purchase()
        {
            var snackMachine = new SnackMachine();
            snackMachine.InsertMoney(OneDollar);
            snackMachine.InsertMoney(OneDollar);
 
            snackMachine.BuySnack(1); // Buy a snack
 
            snackMachine.MoneyInTransaction.Should().Be(None);
            snackMachine.MoneyInside.Amount.Should().Be(2m); // Money inside should be 2 dollars
        }
        [Fact]
        public void BuySnack_trades_inserted_money_for_a_snack()
        {
            var snackMachine = new SnackMachine();
            snackMachine.LoadSnacks(1, new Snack("Layes"), 10, 1m); // Load a snack into the machine
            snackMachine.InsertMoney(OneDollar); // Insert one dollar

            snackMachine.BuySnack(1);

            snackMachine.MoneyInTransaction.Should().Be(None);
            snackMachine.MoneyInside.Amount.Should().Be(1m);
            snackMachine.Slots.Single(x=>x.Position == 1).Quantity.Should().Be(9);
        }
        
    }
}