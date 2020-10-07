using System;
using System.Collections.Generic;

namespace Mediator_Coding_Exercise
{

    public class Participant
    {
        public int Value { get; set; }
        private readonly Mediator mediator;

        public Participant(Mediator mediator)
        {
            this.mediator = mediator;
            mediator.Add(this);
        }

        public void Say(int n)
        {
            mediator.Say(this, n);
        }
    }

    public class Mediator
    {
        private List<Participant> participants = new List<Participant>();
        public void Add(Participant participant)
        {
            participants.Add(participant);
        }

        public void Say(Participant participant, int n)
        {
            foreach (var p in participants)
            {
                if (!p.Equals(participant))
                {
                    p.Value += n;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Mediator m = new Mediator();
            Participant p1 = new Participant(m);
            Participant p2 = new Participant(m);

            p1.Say(2);
            p2.Say(3);

            Console.WriteLine(p1.Value);
            Console.WriteLine(p2.Value);

        }
    }
}
