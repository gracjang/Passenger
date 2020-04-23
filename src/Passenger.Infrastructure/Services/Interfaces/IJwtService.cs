using System;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services.Interfaces
{
  public interface IJwtService
  {
    JwtDto CreateToken(string email, string role);
  }
}