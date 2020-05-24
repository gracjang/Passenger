using System;

namespace Passenger.Infrastructure.Commands
{
  public class AuthenticationCommandBase : IAuthenticationCommand
  {
    public Guid UserId { get; set; }
  }
}