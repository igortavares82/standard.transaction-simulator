using Autofac;
using MediatR;
using Standard.TransactionSimulator.Applicator.Commands;
using System.Reflection;

namespace Standard.TransactionSimulator.Applicator.Configurations
{
    public class MediatorModuleConfiguration : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            Assembly assembly = typeof(SimulateTransationCommand).GetTypeInfo().Assembly;

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IRequestHandler<>));
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.Register<ServiceFactory>(ctx =>
            {
                IComponentContext componentContext = ctx.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });
        }
    }
}
