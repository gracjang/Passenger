using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Passenger.Infrastructure.Services.Interfaces;

namespace Passenger.Infrastructure.Services
{
  public class EncryptionService : IEncryptionService
  {
    private const int SaltLength = 64;
    private const int DeriveBytesIterationsCount = 10000;
    private const int BytesRequested = 32;

    public string GetSalt(string value)
    {
      if(string.IsNullOrEmpty(value))
      {
        throw new ArgumentNullException($"Error occured when getting salt.");
      }

      var salt = new byte[SaltLength];
      var rng = RandomNumberGenerator.Create();
      rng.GetBytes(salt);

      return Convert.ToBase64String(salt);
    }

    public string GetHashPassword(string value, string salt)
    {
      if(string.IsNullOrEmpty(value) || string.IsNullOrEmpty(salt))
      {
        throw new ArgumentNullException($"Error occured when getting hash password.");
      }

      var hashedPassword = Convert.ToBase64String(
        KeyDerivation.Pbkdf2(
          value,
          GetSaltBytes(salt),
          KeyDerivationPrf.HMACSHA512,
          DeriveBytesIterationsCount,
          BytesRequested));

      return hashedPassword;
    }

    private byte[] GetSaltBytes(string value)
    {
      var bytes = new byte[value.Length * sizeof(char)];
      Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);

      return bytes;
    }
  }
}