using System;

namespace Passenger.Infrastructure.Commands.Driver
{
    public class RemoveDriverRouteCommand : AuthenticationCommandBase
    {
        public string Name { get; set; }
    }
}