using System;
using System.ComponentModel;

namespace MethodChain
{
    public class Creature
    {
        public Creature(string name, int attack, int defence)
        {
            Name = name;
            Attack = attack;
            Defence = defence;
        }

        public string Name { get; set; }
        public int Attack { get; set; } 
        public int Defence { get; set; }

        public override string ToString()
        {
            return $"Name:{Name} Attack:{Attack} Defence:{Defence}";
        }
    }

    public class CreatureModifier
    {
        protected Creature creature;
        protected CreatureModifier next; // linked list 

        public CreatureModifier(Creature creature)
        {
            this.creature = creature;
        }
        
        public void Add(CreatureModifier cm)
        {
            if (next != null) next.Add(cm); else next = cm; 
        }

        public virtual void Handle() => next?.Handle();
    }

    public class DoubleAttackModifier : CreatureModifier
    {
        public DoubleAttackModifier(Creature creature) : base(creature)
        {
        }

        public override void Handle()
        {
            Console.WriteLine($"Doubling {creature.Name}'s attack");
            creature.Attack *= 2;
            base.Handle();
        }
    }

    public class IncreaseDefenceModifier : CreatureModifier
    {
        public IncreaseDefenceModifier(Creature creature) : base(creature)
        {
        }

        public override void Handle()
        {
            Console.WriteLine($"Increasing {creature.Name}'s defence");
            creature.Defence += 1;
            base.Handle();
        }
    }

    public class NoBonusesModifier : CreatureModifier
    {
        public NoBonusesModifier(Creature creature) : base(creature)
        {
        }

        public override void Handle()
        {
            Console.WriteLine($"Preventing bonuses to {creature.Name}");

            //base.Handle();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Creature goblin = new Creature("Goblin", 2, 2);
            Console.WriteLine(goblin);
            var root = new CreatureModifier(goblin);
            root.Add(new NoBonusesModifier(goblin));
            Console.WriteLine("Adding Doubling the attack modifier");
            root.Add(new DoubleAttackModifier(goblin));
            Console.WriteLine("Adding Increasing the defence modifer");
            root.Add(new IncreaseDefenceModifier(goblin));
            root.Handle();
            Console.WriteLine(goblin);

        }
    }
}
