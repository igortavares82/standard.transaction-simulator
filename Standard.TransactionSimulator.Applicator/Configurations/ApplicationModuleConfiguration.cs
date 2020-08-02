using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Standand.Framework.MessageBroker.Abstraction;
using Standand.Framework.MessageBroker.Concrete;
using Standand.Framework.MessageBroker.Concrete.Options;

namespace Standard.TransactionSimulator.Applicator.Configurations
{
    public class ApplicationModuleConfiguration : Autofac.Module
    {
        public IConfiguration Configuration { get; }

        public ApplicationModuleConfiguration(IConfiguration configuration) 
        {
            Configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(ctx =>
            {
                return Configuration;
            })
           .As<IConfiguration>()
           .SingleInstance();

            builder.Register(ctx =>
            {
                IOptions<BrokerOptions> broker = ctx.Resolve<IOptions<BrokerOptions>>();
                IComponentContext context = ctx.Resolve<IComponentContext>();

                return new EventBus(context, broker, ConfigureInstancePerLifetimeScope);
            })
            .As<IEventBus>()
            .SingleInstance();
        }

        public void ConfigureInstancePerLifetimeScope(ContainerBuilder builder, IConfiguration configuration)
        {
            /*
            builder.RegisterType<TransactionRepository>()
                   .As<ITransactionRepository>()
                   .InstancePerLifetimeScope();
            */
        }
    }
}
