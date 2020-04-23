using Autofac;
using Microsoft.Extensions.Configuration;
using Passenger.Infrastructure.Extensions;
using Passenger.Infrastructure.Mongo;
using Passenger.Infrastructure.Settings;

namespace Passenger.Infrastructure.IoC.Modules
{
  public class SettingsModule : Module
  {
    private readonly IConfiguration _configuration;
    private const string MongoDatabaseKey = "MongoDatabase";

    public SettingsModule(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterInstance(_configuration.GetSettings<MongoSettings>())
        .As<IMongoSettings>()
        .SingleInstance();

      builder.RegisterInstance(_configuration.GetSettings<JwtSettings>())
        .SingleInstance();
    }
  }
}