using System.Reflection;
using Autofac;

namespace Passenger.Infrastructure.IoC.Modules
{
    public class ProvidersModule : Autofac.Module
    {
    protected override void Load(ContainerBuilder builder)
    {
      var assembly = typeof(ProvidersModule)
        .GetTypeInfo()
        .Assembly;

      builder.RegisterAssemblyTypes(assembly)
        .Where(x => x.Name.EndsWith("Provider"))
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();
    }
    }
}