using Autofac;
using System;
using System.Dynamic;
using System.Threading;
using ImpromptuInterface;

namespace NullObjectDesignPattern
{

    public interface ILog
    {
        void Info(string msg);
        void Warn(string msg);
    }

    public class ConsoleLog : ILog
    {
        public void Info(string msg)
        {
            Console.WriteLine($"Info: {msg}");
        }

        public void Warn(string msg)
        {
            Console.WriteLine($"Warning: {msg}");
        }
    }

    public class BankAccount
    {
        private ILog log;
        private int balance;

        public BankAccount(ILog log)
        {
            this.log = log;
        }

        public void Deposit(int amount)
        {
            balance += amount;
            log.Info($"Deposited {amount} balance is not {balance}");
        }
    }

    public class NullLog : ILog
    {
        public void Info(string msg)
        {
        }

        public void Warn(string msg)
        {
        }
    }

    public class Null<TInterface>:DynamicObject where TInterface:class
    {
        public static TInterface Instance
        { 
            get
            {

                return new Null<TInterface>().ActLike<TInterface>(); 
            }
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = Activator.CreateInstance(binder.ReturnType);
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //var log = new ConsoleLog();
            //var ba = new BankAccount(log);
            //ba.Deposit(100);

            var log = Null<ILog>.Instance;
            var ba = new BankAccount(log);
            ba.Deposit(100);

        }
    }
}
