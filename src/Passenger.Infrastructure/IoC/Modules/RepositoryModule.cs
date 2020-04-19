using Autofac;
using Passenger.Core.Repositories;
using System.Reflection;
using MongoDB.Driver;
using Passenger.Infrastructure.Mongo;
using Module = Autofac.Module;

namespace Passenger.Infrastructure.IoC.Modules
{
  public class RepositoryModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.Register(x =>
      {
        var settings = x.Resolve<IMongoSettings>();

        return new MongoClient(settings.ConnectionString);
      }).SingleInstance();

      builder.Register(x =>
      {
        var client = x.Resolve<MongoClient>();
        var setting = x.Resolve<IMongoSettings>();
        var database = client.GetDatabase(setting.Database);

        return database;
      }).As<IMongoDatabase>();

      var assembly = typeof(RepositoryModule)
        .GetTypeInfo()
        .Assembly;

      builder.RegisterAssemblyTypes(assembly)
        .Where(x => x.IsAssignableTo<IRepository>())
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();
    }
  }
}