﻿namespace Passenger.Infrastructure.Mongo
{
  public class MongoSettings : IMongoSettings
  {
    public string ConnectionString { get; set; }

    public string Database { get; set; }

  }
}