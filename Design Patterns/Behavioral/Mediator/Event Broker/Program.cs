

using Autofac;
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Event_Broker
{
    public class Actor
    {
        protected EventBroker broker;
        public Actor(EventBroker broker)
        {
            this.broker = broker;
        }
    }

    public class FootballPlayer : Actor
    {
        public string Name { get; set; }
        public int GoalsScored { get; set; } = 0;
        public void Score()
        {
            GoalsScored++;
            broker.Publish(new PlayerScoredEvent { Name = Name, GoalsScored = GoalsScored });
        }

        public void Assault()
        {
            broker.Publish(new PlayerSentOffEvent { Name = Name, Reason = "violence" });
        }

        public FootballPlayer(EventBroker broker, string name) : base(broker)
        {
            Name = name;
            broker.OfType<PlayerScoredEvent>()
                .Where(ps => !ps.Name.Equals(name))
                .Subscribe(ps => Console.WriteLine($"{name}: Well done {ps.Name} - your {ps.GoalsScored} goal."));

            broker.OfType<PlayerSentOffEvent>()
                .Where(ps => !ps.Name.Equals(name))
                .Subscribe(ps => Console.WriteLine($"{name}: See you in locker room {ps.Name}"));
        }

    }

    public class FootballCoach : Actor
    {
        public FootballCoach(EventBroker broker) : base(broker)
        {
            broker.OfType<PlayerScoredEvent>().Subscribe(pe =>
            {
                if (pe.GoalsScored < 3)
                {
                    Console.WriteLine($"Coach: Well done {pe.Name}");
                }
            });

            broker.OfType<PlayerSentOffEvent>().Subscribe(pe =>
            {
                if (pe.Reason == "violence")
                    Console.WriteLine($"Coach: shameful {pe.Name}");
            });
        }
    }

    public class PlayerEvent
    {
        public string Name { get; set; }
    }

    public class PlayerScoredEvent : PlayerEvent
    {
        public int GoalsScored { get; set; }
    }

    public class PlayerSentOffEvent : PlayerEvent
    {
        public string Reason { get; set; }
    }

    public class EventBroker : IObservable<PlayerEvent>
    {
        Subject<PlayerEvent> subscriptions = new Subject<PlayerEvent>();

        public IDisposable Subscribe(IObserver<PlayerEvent> observer)
        {
            return subscriptions.Subscribe(observer);
        }

        public void Publish(PlayerEvent pe)
        {
            subscriptions.OnNext(pe);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<EventBroker>().SingleInstance();
            cb.RegisterType<FootballCoach>();
            cb.Register((c, p) => new FootballPlayer(
                c.Resolve<EventBroker>(),
                p.Named<string>("name")
                ));

            using (var c = cb.Build())
            {
                var coach = c.Resolve<FootballCoach>();
                var p1 = c.Resolve<FootballPlayer>(new NamedParameter("name", "Philip"));
                var p2 = c.Resolve<FootballPlayer>(new NamedParameter("name", "Alexander"));
                p1.Score();
                p1.Score();
                p1.Score();
                p1.Assault();
                p2.Score();
            }
        }
    }
}
