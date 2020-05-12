using Passenger.Infrastructure.Commands.Driver.Models;

namespace Passenger.Infrastructure.Commands.Driver
{
    public class AddDriverCommand : AuthenticationCommandBase
    {
        public DriverVehicle Vehicle { get; set;}
    }
}