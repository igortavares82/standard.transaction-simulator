using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Standard.TransactionSimulator.Applicator.Commands;
using System.Timers;
using System.Threading.Tasks;

namespace Standard.TransactionSimulator.Applicator.Configurations
{
    public static class StartSimulatorConfiguration
    {
        public static void StartSimulator(this IApplicationBuilder app) 
        {
            IMediator mediator = app.ApplicationServices.GetRequiredService<IMediator>();
         
            Timer timer = new Timer(1000);
            timer.Elapsed += (object source, ElapsedEventArgs e) => { mediator.Send(new SimulateTransationCommand()); };
            timer.AutoReset = true;
            timer.Enabled = true;
        }
    }
}
