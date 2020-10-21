using System;

namespace TemplateCodingExercise
{
    public class Creature
    {
        public int Attack, Health;

        public Creature(int attack, int health)
        {
            Attack = attack;
            Health = health;
        }
    }

    public abstract class CardGame
    {
        public Creature[] Creatures;

        public CardGame(Creature[] creatures)
        {
            Creatures = creatures;
        }

        // returns -1 if no clear winner (both alive or both dead)
        public int Combat(int creature1, int creature2)
        {
            Creature first = Creatures[creature1];
            Creature second = Creatures[creature2];
            Hit(first, second);
            Hit(second, first);
            bool firstAlive = first.Health > 0;
            bool secondAlive = second.Health > 0;
            if (firstAlive == secondAlive) return -1;
            return firstAlive ? creature1 : creature2;
        }

        // attacker hits other creature
        protected abstract void Hit(Creature attacker, Creature other);
    }

    public class TemporaryCardDamageGame : CardGame
    {
        // todo
        public TemporaryCardDamageGame(Creature[] creatures) : base(creatures) { }

        protected override void Hit(Creature a, Creature b)
        {
            if (a.Health - b.Attack <= 0) a.Health = 0;
        }
    }

    public class PermanentCardDamage : CardGame
    {
        // todo
        public PermanentCardDamage(Creature[] creatures) : base(creatures) { }
        protected override void Hit(Creature a, Creature b)
        {
            a.Health -= b.Attack;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
