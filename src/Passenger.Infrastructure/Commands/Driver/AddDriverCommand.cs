using System;

namespace Passenger.Infrastructure.Commands.Driver
{
    public class AddDriverCommand : AuthenticationCommandBase
    {
        public DriverVehicle Vehicle { get; set;}

        public class DriverVehicle
        {
            public string Brand { get; set; }

            public string Name { get; set; }
        }
    }
}