using System;

namespace Passenger.Infrastructure.Commands
{
    public interface IAuthenticationCommand : ICommand
    {
         public Guid UserId { get; set; }
    }
}