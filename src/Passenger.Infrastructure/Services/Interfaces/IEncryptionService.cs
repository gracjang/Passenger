namespace Passenger.Infrastructure.Services.Interfaces
{
  public interface IEncryptionService
  {
    string GetSalt(string value);

    string GetHashPassword(string value, string salt);
  }
}