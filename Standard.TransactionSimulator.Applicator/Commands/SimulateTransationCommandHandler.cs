using MediatR;
using Microsoft.Extensions.Options;
using Standand.Framework.MessageBroker.Abstraction;
using Standard.Stock.Event;
using Standard.TransactionSimulator.Applicator.Options;
using System.Threading;
using System.Threading.Tasks;

namespace Standard.TransactionSimulator.Applicator.Commands
{
    public class SimulateTransationCommandHandler : IRequestHandler<SimulateTransationCommand>
    {
        private IEventBus EventBus { get; }
        private TrendingOptions TrendingOptions { get; }

        public SimulateTransationCommandHandler(IEventBus eventBus, IOptions<TrendingOptions> trendingOptions) 
        {
            EventBus = eventBus;
            TrendingOptions = trendingOptions.Value;
        }

        public async Task<Unit> Handle(SimulateTransationCommand request, CancellationToken cancellationToken)
        {
            TrendingRequestEvent trendingRequest = new TrendingRequestEvent();
            await EventBus.CallAsync<TrendingRequestEvent,TrendingResponseEvent>(trendingRequest, TrendingOptions);

            return Unit.Value;
        }
    }
}
