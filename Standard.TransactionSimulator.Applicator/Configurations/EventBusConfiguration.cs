using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Standand.Framework.MessageBroker.Abstraction;
using Standard.TransactionSimulator.Applicator.Options;

namespace Standard.TransactionSimulator.Applicator.Configurations
{
    public static class EventBusConfiguration
    {
        public static void ConfigureEventBus(this IApplicationBuilder app)
        {
            IEventBus eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            IOptions<TrendingOptions> options = app.ApplicationServices.GetRequiredService<IOptions<TrendingOptions>>();
        }
    }
}
