using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandDesignPattern
{
    public class BankAccount
    {
        public int balance;
        public int overdraftLimit = -500;
        public void Deposit(int amount) { balance += amount; Console.WriteLine($"Deposit {amount} New balance: {balance}"); }
        public bool WithDraw(int amount) { 
            if (balance - amount > overdraftLimit) { 
                balance -= amount; 
                Console.WriteLine($"Withdraw {amount} New balance: {balance}"); 
                return true; 
            } 
            return false; 
        }

        public override string ToString()
        {
            return $"Balance: {balance}";
        }
    }

    public interface ICommand
    {
        void Call();
        void Undo();
    }

    public class BankAccountCommand:ICommand
    {
        private BankAccount account;
        public enum Action { Deposit, Withdraw }
        private Action action;
        private int amount;
        private bool succeeded;

        public BankAccountCommand(BankAccount account, Action action, int amount)
        {
            this.account = account;
            this.action = action;
            this.amount = amount;
        }

        public void Call()
        {
            
            switch (action)
            {
                case Action.Deposit:
                    account.Deposit(amount);
                    succeeded = true;
                    break;
                case Action.Withdraw:
                    succeeded = account.WithDraw(amount);
                    break;
                default:
                    break;
            }
        }

        public void Undo()
        {
            if (!succeeded) return;
            Console.WriteLine("Undoing call ");
            switch (action)
            {
                case Action.Deposit:
                    account.WithDraw(amount);
                    break;
                case Action.Withdraw:
                    account.Deposit(amount);
                    break;
                default:
                    break;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var ba = new BankAccount();
            var commands = new List<BankAccountCommand>()
            {
                new BankAccountCommand(ba,BankAccountCommand.Action.Deposit,100),
                new BankAccountCommand(ba,BankAccountCommand.Action.Withdraw,1150)

            };

            Console.WriteLine(ba);
            foreach (var c in commands)
            {
                c.Call();
            }
            Console.WriteLine(ba);

            foreach (var c in Enumerable.Reverse(commands))
            {
               
                c.Undo();
            }

            Console.WriteLine(ba);
        }
    }
}
