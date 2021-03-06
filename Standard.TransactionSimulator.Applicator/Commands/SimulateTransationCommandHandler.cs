﻿using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Standand.Framework.MessageBroker.Abstraction;
using Standard.Stock.Event;
using Standard.TransactionSimulator.Applicator.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Standard.TransactionSimulator.Applicator.Commands
{
    public class SimulateTransationCommandHandler : IRequestHandler<SimulateTransationCommand>
    {
        private IEventBus EventBus { get; }
        private TransactionOptions TransactionOptions { get; }
        private IConfiguration Configuration { get; }

        public SimulateTransationCommandHandler(IEventBus eventBus, 
                                                IOptions<TransactionOptions> transactionOptions,
                                                IConfiguration configuration) 
        {
            EventBus = eventBus;
            TransactionOptions = transactionOptions.Value;
            Configuration = configuration;
        }

        public async Task<Unit> Handle(SimulateTransationCommand request, CancellationToken cancellationToken)
        {
            TrendingRequestEvent trendingRequest = new TrendingRequestEvent();

            IList<ReceiveTransactionEvent> events = GenerateData(request.QuantityOfEvents);
            events.ToList().ForEach(async it => await EventBus.PublishAsync(it, TransactionOptions));
            
            return Unit.Value;
        }

        private IList<ReceiveTransactionEvent> GenerateData(int? quantity) 
        {
            IList<ReceiveTransactionEvent> events = new List<ReceiveTransactionEvent>();

            string[] stocks = Configuration.GetSection("keys:stocks").Get<string[]>();
            int totalEvents = 0;

            if (!quantity.HasValue)
                totalEvents = new Random().Next(1, 10);
            else
                totalEvents = quantity.Value;

            for (int i = 0; i < totalEvents; i++)
            {
                string stock = stocks[new Random().Next(0, stocks.Length - 1)];

                ReceiveTransactionEvent receiveTransaction = new ReceiveTransactionEvent()
                {
                    Initials = stock,
                    Type = new Random().Next(1, 3),
                    Quantity = new Random().Next(1, 1000),
                    Price = GetPrice(null)
                };

                events.Add(receiveTransaction);
            }

            return events;
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
