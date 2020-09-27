using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Dynamic_Proxy_for_Logging
{
    public interface IBankAccount
    {
        void Deposit(int amount);
        bool Withdraw(int amount);
        string ToString();
    }

    public class BankAccount:IBankAccount
    {
        private int balance;
        private int overdraftLimit = -500;

        public void Deposit(int amount)
        {
            balance += amount;
            Console.WriteLine($"Deposited {amount}, balance is not {balance}");
        }

        public bool Withdraw(int amount)
        {
            if (balance - amount >= overdraftLimit)
            {
                balance -= amount;
                Console.WriteLine($"Withdrew {amount}, balance is not {balance}");
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"Bank Account Balance: {balance}";
        }
    }

    public class Log<T>:DynamicObject where T:class, new()
    {
        private readonly T subject;
        private Dictionary<string, int> methodCallCount = new Dictionary<string, int>();
        public Log (T subject) { this.subject = subject; }
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            try
            {
                Console.WriteLine($"Invoking {subject.GetType().Name}.{binder.Name} with arguments [{string.Join(",",args)}]");
                if (methodCallCount.ContainsKey(binder.Name)) methodCallCount[binder.Name]++;
                else methodCallCount.Add(binder.Name, 1);
                result = subject.GetType().GetMethod(binder.Name).Invoke(subject, args);
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            } 
        }

        public string Info
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var kv in methodCallCount)
                {
                    sb.AppendLine($"{kv.Key} calles {kv.Value} time(s)");
                }
                return sb.ToString();
            }
        }

        public override string ToString()
        {
            return $"{Info}\n{subject}";

        }
    }



    class Program
    {


        static void Main(string[] args)
        {
            var ba = new BankAccount();
            ba.Deposit(100);
            ba.Withdraw(50);
            Console.WriteLine(ba);
        }
    }
}
