using Autofac;
using Microsoft.Extensions.Configuration;
using Passenger.Infrastructure.Mongo;

namespace Passenger.Infrastructure.IoC.Modules
{
  public class SettingsModule : Module
  {
    private readonly IConfiguration _configuration;

    public SettingsModule(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterInstance(_configuration.GetSection("MongoDatabase").Get<MongoSettings>())
        .As<IMongoSettings>()
        .SingleInstance();
    }
  }
}