using System;

namespace ObserverCodingExercise
{

    public class Game
    {
        // todo
        // remember - no fields or properties!
        // the game observes and notifies the rats
        public event EventHandler<Rat> NotifyAllRatsForAddition, NotifyAllRatsForRemoval;
        public event EventHandler<Rat> NotifyEnteringRat;
        public void FireNotifyAllRatsForAddition(Rat rat)
        {
            NotifyAllRatsForAddition?.Invoke(this, rat);
        }

        public void FireNotifyRatsForRemoval(Rat rat)
        {
            NotifyAllRatsForRemoval?.Invoke(this, rat);
        }

        public void FireNotifyEnteringRat(object sender, Rat rat)
        {
            NotifyEnteringRat?.Invoke(sender, rat);
        }

    }

    public class Rat : IDisposable
    {
        public int Attack = 1;
        private readonly Game _game;

        public Rat(Game game)
        {
            _game = game;
            _game.NotifyAllRatsForAddition += (sender, ratArgs) =>
            {
                if (ratArgs != this) {
                    ++Attack;

                    _game.FireNotifyEnteringRat(this, (Rat)sender);
                }
            };

            _game.NotifyAllRatsForRemoval += (sender, ratArgs) =>
            {
                if (ratArgs != this) --Attack;
            };
            
            _game.FireNotifyAllRatsForAddition(this);
            
        }

        public void Dispose()
        {
            _game.FireNotifyRatsForRemoval(this);
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            Game g = new Game();
            Rat r1 = new Rat(g); Rat r2 = new Rat(g);

            Console.WriteLine(r1.Attack);
            Console.WriteLine(r2.Attack);
        }
    }
}
