using Autofac;
using System;

namespace DecoratorInDependencyInjection
{
    class Program
    {
        public interface IReportingService
        {
            void Report();
        }

        public class ReportingService : IReportingService
        {
            public void Report()
            {
                Console.WriteLine("Here is your report");
            }
        }

        public class ReportingServiceWithLogging : IReportingService
        {
            public IReportingService decorated;

            public ReportingServiceWithLogging(IReportingService decorated)
            {
                this.decorated = decorated;
            }

            public void Report()
            {
                Console.WriteLine("Start Logging your report");
                decorated.Report();
                Console.WriteLine("End Logging your report");

            }
        }


        static void Main(string[] args)
        {
            var b = new ContainerBuilder();
            b.RegisterType<ReportingService>().Named<IReportingService>("reporting");
            b.RegisterDecorator<IReportingService>(
                (context, service) => new ReportingServiceWithLogging(service), "reporting"
            );
            using var c = b.Build() ;
            var r = c.Resolve<IReportingService>();
            r.Report();



        }
    }
}
