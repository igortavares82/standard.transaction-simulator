using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Standand.Framework.MessageBroker.Abstraction;
using Standard.Stock.Event;
using Standard.TransactionSimulator.Applicator.Options;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Standard.TransactionSimulator.Applicator.Commands
{
    public class SimulateTransationCommandHandler : IRequestHandler<SimulateTransationCommand>
    {
        private IEventBus EventBus { get; }
        private TrendingOptions TrendingOptions { get; }
        private TransactionOptions TransactionOptions { get; }
        private IConfiguration Configuration { get; }

        public SimulateTransationCommandHandler(IEventBus eventBus, 
                                                IOptions<TrendingOptions> trendingOptions,
                                                IOptions<TransactionOptions> transactionOptions,
                                                IConfiguration configuration) 
        {
            EventBus = eventBus;
            TrendingOptions = trendingOptions.Value;
            TransactionOptions = transactionOptions.Value;
            Configuration = configuration;
        }

        public async Task<Unit> Handle(SimulateTransationCommand request, CancellationToken cancellationToken)
        {
            TrendingRequestEvent trendingRequest = new TrendingRequestEvent();
            TrendingResponseEvent trendingResponse = null;// await EventBus.CallAsync<TrendingRequestEvent,TrendingResponseEvent>(trendingRequest, TrendingOptions);

            string[] stocks = Configuration.GetSection("stocks").Get<string[]>();
            int totalEvents = new Random().Next(1, 10);

            for (int i = 0; i < totalEvents; i++) 
            {
                string stock = stocks[new Random().Next(0, stocks.Length - 1)];

                ReceiveTransactionEvent receiveTransaction = new ReceiveTransactionEvent() 
                {
                    Initials = stock,
                    Type = new Random().Next(1, 3),
                    Quantity = new Random().Next(1, 1000),
                    Price = GetPrice(trendingResponse?.Trendings.FirstOrDefault(it => it.Initials == stock))
                };

                await EventBus.PublishAsync(receiveTransaction, TransactionOptions);
            }

            return Unit.Value;
        }

        private decimal GetPrice(TrendingItemEvent trendingItemEvent) 
        {
            if (trendingItemEvent == null)
                return new Random().Next(10, 50);

            decimal factor = (decimal)(new Random().Next(1, 3)) / 100 + new Random().Next(0, 2);
            return trendingItemEvent.Average * factor;
        }
    }
}
