using Passenger.Infrastructure.Commands.Driver.Models;

namespace Passenger.Infrastructure.Commands.Driver
{
    public class UpdateDriverCommand : AuthenticationCommandBase
    {
        public DriverVehicle Vehicle { get; set; }
    }
}