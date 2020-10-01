using System;
using System.Collections.Generic;
using System.Linq;

namespace CompositeCommand
{
    public class BankAccount
    {
        public int balance;
        public int overdraftLimit = -500;
        public void Deposit(int amount) { balance += amount; Console.WriteLine($"Deposit {amount} New balance: {balance}"); }
        public bool WithDraw(int amount)
        {
            if (balance - amount > overdraftLimit)
            {
                balance -= amount;
                Console.WriteLine($"Withdraw {amount} New balance: {balance}");
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"BankAccount Status: Balance: {balance}";
        }
    }
    public interface ICommand
    {
        void Call();
        void Undo();
        bool Success { get; set; }
    }

    public class BankAccountCommand : ICommand
    {
        private BankAccount account;
        public enum Action { Deposit, Withdraw }
        private Action action;
        private int amount;
        

        public bool Success { get; set; }

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
                    Success = true;
                    break;
                case Action.Withdraw:
                    Success = account.WithDraw(amount);
                    break;
                default:
                    break;
            }
        }

        public void Undo()
        {
            if (!Success) return;
            Console.Write("Undoing call. ");
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

    public class CompositeBankAccountCommand : List<BankAccountCommand>, ICommand
    {
        public CompositeBankAccountCommand()
        {

        }

        public CompositeBankAccountCommand(IEnumerable<BankAccountCommand> collection):base(collection)
        {

        }

        public virtual bool Success
        {
            get => this.All(cmd => cmd.Success);
            set { foreach (var cmd in this) cmd.Success = value; }
        }

        public virtual void Call()
        {
            ForEach(cmd => cmd.Call());
        }

        public virtual void Undo()
        {
            foreach (var cmd in ((IEnumerable<BankAccountCommand>)this).Reverse())
            {
                if (cmd.Success) cmd.Undo();
            }
        }
    }

    public class MoneyTransferCommand:CompositeBankAccountCommand
    {
        public MoneyTransferCommand(BankAccount from, BankAccount to, int amount)
        {
            AddRange(new[]
            {
                new BankAccountCommand(from, BankAccountCommand.Action.Withdraw, amount),
                new BankAccountCommand(to, BankAccountCommand.Action.Deposit,amount)
            });
        }

        public override void Call()
        {
            BankAccountCommand last = null;
            foreach (var cmd in this)
            {
                if (last == null || last.Success)
                {
                    cmd.Call();
                    last = cmd;
                }
                else
                {
                    cmd.Undo();
                    break;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //var ba = new BankAccount();
            //var deposit = new BankAccountCommand(ba, BankAccountCommand.Action.Deposit, 100);
            //var withdraw = new BankAccountCommand(ba, BankAccountCommand.Action.Withdraw, 50);
            //var composite = new CompositeBankAccountCommand(new[] { deposit, withdraw } );
            //composite.Call();
            //Console.WriteLine(ba);
            //composite.Undo();
            //Console.WriteLine(ba);

            var from = new BankAccount();
            var to = new BankAccount();
            Console.WriteLine("Transferring money to from account");
            from.Deposit(100);
            var mtc = new MoneyTransferCommand(from, to, 1000);
            Console.WriteLine();
            Console.WriteLine("Initiating money transfer");
            mtc.Call();
            Console.WriteLine($"Account 'From': {from}");
            Console.WriteLine($"Account 'To': {to}");
            Console.WriteLine();
            Console.WriteLine("Undoing money transfer");
            mtc.Undo();
            Console.WriteLine($"Account 'From': {from}");
            Console.WriteLine($"Account 'To': {to}");





        }
    }
}
