using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Passenger.Infrastructure.IoC.Modules
{
  public class ServiceModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      var assembly = typeof(ServiceModule).
        GetTypeInfo()
        .Assembly;

      builder.RegisterAssemblyTypes(assembly)
        .Where(x => x.Name.EndsWith("Service"))
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();
    }
  }
}