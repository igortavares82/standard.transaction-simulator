using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Standand.Framework.MessageBroker.Concrete.Options;
using Standard.TransactionSimulator.Applicator.Options;

namespace Standard.TransactionSimulator.Applicator.Configurations
{
    public static class OptionsConfiguration
    {
        public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BrokerOptions>(options => configuration.GetSection("brokerOptions").Bind(options));
            services.Configure<TrendingOptions>(options => configuration.GetSection("channels:trending:get").Bind(options));
            services.Configure<TransactionOptions>(options => configuration.GetSection("channels:transaction:send").Bind(options));
        }
    }
}
