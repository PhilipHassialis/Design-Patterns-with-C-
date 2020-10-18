using Stateless;
using System;

namespace State_Machine_With_Stateless
{
    public enum Health
    {
        NonReproductive,
        Pregnant,
        Reproductive
    }

    public enum Activity
    {
        GiveBirth,
        ReachPuberty,
        HaveAbortion,
        HaveUnprotectedSex,
        Hysterectomy
    }

    class Program
    {
        public static bool ParentsNotWatching { get; private set; }

        static void Main(string[] args)
        {
            var machine = new StateMachine<Health, Activity>(Health.NonReproductive);
            machine.Configure(Health.NonReproductive)
                .Permit(Activity.ReachPuberty, Health.Reproductive);
            machine.Configure(Health.Reproductive)
                .Permit(Activity.Hysterectomy, Health.NonReproductive)
                .PermitIf(Activity.HaveUnprotectedSex, Health.Pregnant, ()=>ParentsNotWatching);

            machine.Configure(Health.Pregnant)
                .Permit(Activity.GiveBirth, Health.Reproductive)
                .Permit(Activity.HaveAbortion, Health.Reproductive);

        }
    }
}
