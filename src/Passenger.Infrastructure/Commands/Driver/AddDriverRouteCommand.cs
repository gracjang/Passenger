using System;

namespace Passenger.Infrastructure.Commands.Driver
{
    public class AddDriverRouteCommand : AuthenticationCommandBase
    {
        public string Name { get; set; }
        public double StartLatitude { get; set; }
        public double StartLongitude { get; set; }
        public double EndLatitude { get; set; }
        public double EndLongitude { get; set; }
        public double Distance { get; set; }
    }
}