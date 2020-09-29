using ChainOfResponsibilityCodingExercise;
using System;
using System.Collections.Generic;

namespace ChainOfResponsibilityCodingExercise
{

    public abstract class Creature
    {
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int AttackModifier = 0;
        public int DefenseModifier = 0;

        public void AddToGame(Game game)
        {
            foreach (var c in game.Creatures)
            {
                c.Attack += AttackModifier;
                c.Defense += DefenseModifier;
            }
        }

        public void RemoveFromGame(Game game)
        {
            foreach (var c in game.Creatures)
            {
                c.Attack -= AttackModifier;
                c.Defense -= DefenseModifier;
            }

        }

    }

    public class Goblin : Creature
    {


        public Goblin(Game game)
        {
            Attack = 1; Defense = 1;
            DefenseModifier = 1;
            AddToGame(game);
        }


    }

    public class GoblinKing : Goblin
    {
        public GoblinKing(Game game) : base(game)
        {
            Attack = 3; Defense = 3; AttackModifier = 1; AddToGame(game);

        }
    }

    public class Game
    {
        public IList<Creature> Creatures;

        public Game()
        {
            Creatures = new List<Creature>();
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Game g = new Game();
            Goblin goblin = new Goblin(g);
            Goblin goblin2 = new Goblin(g);

            


        }


    }
}



