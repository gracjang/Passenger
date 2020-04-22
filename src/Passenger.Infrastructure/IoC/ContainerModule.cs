using Autofac;
using Microsoft.Extensions.Configuration;
using Passenger.Infrastructure.IoC.Modules;

namespace Passenger.Infrastructure.IoC
{
  public class ContainerModule : Module
  {
    private readonly IConfiguration _configuration;

    public ContainerModule(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterModule(new SettingsModule(_configuration));
      builder.RegisterModule<RepositoryModule>();
      builder.RegisterModule<ConverterModule>();
      builder.RegisterModule<ServiceModule>();
      builder.RegisterModule<CommandModule>();
    }
  }
}