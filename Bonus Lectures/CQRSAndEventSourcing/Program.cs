using System;
using System.Collections.Generic;
using System.Linq;

namespace CQRSAndEventSourcing
{

    // CQRS Command Query Responsibility Separation
    // CQS Command Query Separation

    // COMMAND -> do/change

    public class PersonStorage
    {
        public Dictionary<int, Person> People;
    }

    public class Person
    {
        private int age;
        public int UniqueId;
        EventBroker broker;
        public Person(EventBroker broker)
        {
            this.broker = broker;
            broker.Commands += BrokerOnCommands;
            broker.Queries += BrokerOnQueries;
        }

        private void BrokerOnQueries(object sender, Query query)
        {
            var ac = query as AgeQuery;
            if (ac!=null && ac.Target==this)
            {
                ac.Result = age;
            }
        }

        private void BrokerOnCommands(object sender, Command command)
        {
            var cac = command as ChangeAgeCommand;
            if (cac!=null && cac.target==this)
            {
                if (cac.Register) broker.AllEvents.Add(new AgeChangedEvent(this, age, cac.Age));
                age = cac.Age;
            }
        }
    }

    public class EventBroker
    {
        // 1. All Events that happened
        public IList<Event> AllEvents = new List<Event>();
        // 2. Commands
        public event EventHandler<Command> Commands;
        // 3. Query
        public event EventHandler<Query> Queries;

        public void Command(Command c)
        {
            Commands?.Invoke(this, c);
        }

        public T Query<T>(Query q)
        {
            Queries?.Invoke(this, q);
            return (T)q.Result;
        }

        public void UndoLast()
        {
            var e = AllEvents.LastOrDefault();
            var ac = e as AgeChangedEvent;
            if (ac!=null)
            {
                Command(new ChangeAgeCommand(ac.Target, ac.oldValue) { Register = false});
                AllEvents.Remove(e);
            }
        }

    }

    public class Query
    {
        public object Result;
    }

    public class AgeQuery : Query 
    {
        public Person Target;
    }

    public class Command:EventArgs
    {
        public bool Register = true;
    }

    public class ChangeAgeCommand : Command
    {
        public Person target;
        public int Age;

        public ChangeAgeCommand(Person target, int age)
        {
            this.target = target;
            Age = age;
        }


    }


    public class Event
    {
        // backtrack
    }

    public class AgeChangedEvent: Event
    {
        public Person Target;
        public int oldValue, newValue;

        public AgeChangedEvent(Person target, int oldValue, int newValue)
        {
            Target = target;
            this.oldValue = oldValue;
            this.newValue = newValue;
        }
        public override string ToString()
        {
            return $"Age change from {oldValue} to {newValue}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var eb = new EventBroker();

            var p = new Person(eb);
            //p.Age = 45; //  <-- HERE

            Console.WriteLine("Sending changeage command" );
            eb.Command(new ChangeAgeCommand(p, 45));
            Console.WriteLine("all events:");
            foreach (var e in eb.AllEvents)
            {
                Console.WriteLine(e);
            }

            int age;
            age = eb.Query<int>(new AgeQuery { Target = p });
            Console.WriteLine($"Age: {age}");

            Console.WriteLine("Perfoming undo");
            eb.UndoLast();
            Console.WriteLine("all events:");
            foreach (var e in eb.AllEvents)
            {
                Console.WriteLine(e);
            }

            age = eb.Query<int>(new AgeQuery { Target = p });

            Console.WriteLine($"Age: {age}");

        }
    }
}
