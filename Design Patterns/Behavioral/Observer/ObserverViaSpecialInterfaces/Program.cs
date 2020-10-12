using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;

namespace ObserverViaSpecialInterfaces
{
    public class Event
    {

    }

    public class FallsIllEvent : Event
    {
        public string Adresss;
    }

    public class Person : IObservable<Event>
    {
        private readonly HashSet<Subscription> subscriptions = new HashSet<Subscription>();

        private class Subscription : IDisposable
        {
            private readonly Person person;
            public readonly IObserver<Event> Observer;
            public Subscription(Person person, IObserver<Event> observer)
            {
                this.person = person;
                Observer = observer;
            }
            public void Dispose()
            {
                person.subscriptions.Remove(this);
            }
        }

        public IDisposable Subscribe(IObserver<Event> observer)
        {
            var subscription = new Subscription(this, observer);
            subscriptions.Add(subscription);
            return subscription;
        }

        public void FallIll()
        {
            foreach (var s in subscriptions)
            {
                s.Observer.OnNext(new FallsIllEvent { Adresss = "Megistis 30A" });
            }
        }
    }


    class Program:IObserver<Event>
    {
        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            var person = new Person();
            //var sub = person.Subscribe(this);

            person.OfType<FallsIllEvent>().Subscribe(args => Console.WriteLine($"A doctor oftype is required at {args.Adresss}"));


            person.FallIll();

        }

        public void OnCompleted()
        {
            // called when no more events can be generated
        }

        public void OnError(Exception error)
        {
            // only for error
        }

        public void OnNext(Event value)
        {
            if (value is FallsIllEvent args)
            {
                Console.WriteLine($"A doctor is required at {args.Adresss}");
            }
        }
    }
}
