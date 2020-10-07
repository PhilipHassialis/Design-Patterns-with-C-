using Autofac;
using JetBrains.Annotations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR
{
    public class PingCommand : IRequest<PongResponse>
    {

    }

    public class PongResponse
    {
        public DateTime TimeStamp;

        public PongResponse(DateTime timeStamp)
        {
            TimeStamp = timeStamp;
        }
    }

    [UsedImplicitly]
    public class PingCommandHandler : IRequestHandler<PingCommand, PongResponse>
    {
        public async Task<PongResponse> Handle(PingCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new PongResponse(DateTime.UtcNow)).ConfigureAwait(false);
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {

            var builder = new ContainerBuilder();
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();
            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);

            });
            builder.RegisterAssemblyTypes(typeof(Program).Assembly).AsImplementedInterfaces();
            var container = builder.Build();
            var mediator = container.Resolve<IMediator>();
            var response = await mediator.Send(new PingCommand());
            Console.WriteLine($"We got a response at {response.TimeStamp}");


        }
    }
}
