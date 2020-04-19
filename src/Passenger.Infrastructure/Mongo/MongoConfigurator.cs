using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;

namespace Passenger.Infrastructure.Mongo
{
  public class MongoConfigurator
  {
    private static bool _initialized;

    public static void Initialize()
    {
      if(_initialized)
      {
        return;
      }

      _initialized = true;
    }

    private static void RegisterConventions()
    {
      ConventionRegistry.Register("PassengerConventions", new MongoConvention(),x => true);

    }

    private class MongoConvention : IConventionPack
    {
      public IEnumerable<IConvention> Conventions => new List<IConvention>()
      {
        new IgnoreExtraElementsConvention(true),
        new CamelCaseElementNameConvention(),
        new EnumRepresentationConvention(BsonType.String),
      };
    }
  }
}