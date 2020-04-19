namespace Passenger.Infrastructure.Mongo
{
  public interface IMongoSettings
  {
    string ConnectionString { get; set; }

    string Database { get; set; }
  }
}