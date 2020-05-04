using System;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services.Interfaces
{
  public interface IJwtService
  {
    JwtDto CreateToken(Guid userId, string role);
  }
}