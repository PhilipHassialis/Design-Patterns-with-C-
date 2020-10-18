using System;
using System.Text;

namespace StateCodingExercise
{

    public class CombinationLock
    {
        enum State
        {
            LOCKED,
            OPEN,
            ERROR
        }

        State state;
        StringBuilder sbStatus, sbCombination;

        public CombinationLock(int[] combination)
        {
            state = State.LOCKED;
            sbStatus = new StringBuilder(state.ToString());
            sbCombination = new StringBuilder();
            Status = state.ToString();
            foreach (var c in combination)
            {
                sbCombination.Append(c);
            }
        }

        // you need to be changing this on user input
        public string Status;

        public void EnterDigit(int digit)
        {
            switch (state)
            {
                case State.LOCKED:
                    if (Status.Equals(State.LOCKED.ToString())) sbStatus.Clear();
                    if (sbStatus.Length < sbCombination.Length)
                    {
                        sbStatus.Append(digit);
                        if (sbStatus.Length == sbCombination.Length)
                        {
                            state = (sbStatus.ToString() == sbCombination.ToString()) ? State.OPEN : State.ERROR;
                            Status = state.ToString();
                        }
                        else
                        {
                            Status = sbStatus.ToString();
                        }
                    } 
                    break;
                case State.OPEN:
                    Status = state.ToString();
                    break;
                case State.ERROR:
                    Status = state.ToString();
                    break;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var cl = new CombinationLock(new int[] { 1, 2, 3, 4, 5 });
            Console.WriteLine(cl.Status);
            cl.EnterDigit(1);
            Console.WriteLine(cl.Status);
            cl.EnterDigit(2);
            Console.WriteLine(cl.Status);
            cl.EnterDigit(3);
            Console.WriteLine(cl.Status);
            cl.EnterDigit(4);
            Console.WriteLine(cl.Status);
            cl.EnterDigit(5);
            Console.WriteLine(cl.Status);

        }
    }
}
