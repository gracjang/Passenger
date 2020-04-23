using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Passenger.Infrastructure.IoC.Modules
{
  public class ConverterModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      var assembly = typeof(ConverterModule)
        .GetTypeInfo()
        .Assembly;

      builder.RegisterAssemblyTypes(assembly)
        .Where(x => x.Name.EndsWith("Converter"))
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();
    }
  }
}