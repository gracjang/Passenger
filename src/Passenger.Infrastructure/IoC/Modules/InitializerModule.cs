using System.Reflection;
using Autofac;

namespace Passenger.Infrastructure.IoC.Modules
{
  public class InitializerModule : Autofac.Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      var assembly = typeof(InitializerModule).GetTypeInfo()
        .Assembly;

      builder.RegisterAssemblyTypes(assembly)
        .Where(x => x.Name.EndsWith("Initializer"))
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();
    }
  }
}