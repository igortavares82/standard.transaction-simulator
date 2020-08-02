using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Standard.TransactionSimulator.Applicator.Commands;

namespace Standard.TransactionSimulator.Applicator.Configurations
{
    public static class StartSimulatorConfiguration
    {
        public static void StartSimulator(this IApplicationBuilder app) 
        {
            IMediator mediator = app.ApplicationServices.GetRequiredService<IMediator>();
            mediator.Send(new SimulateTransationCommand());
        }
    }
}
