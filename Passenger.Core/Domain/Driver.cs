using System;
using System.Collections.Generic;

namespace Passenger.Core.Domain
{
    public class Driver
    {
        public Guid UserId { get; protected set; }
        public string Username { get; set; }
        public Vehicle Vehicle { get; protected set; }
        public IEnumerable<Route> Routes { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        protected Driver()
        {   
        }

        public Driver(User user)
        {
            UserId = user.Id;
            Username = user.Username;
        }

        public void SetVehicle(Vehicle vehicle)
        {
            Vehicle = vehicle;
            UpdatedAt = UpdatedAt;
        }

        public void AddRoute(Route route)
        { 
        }

        public void DeleteRoute(string name)
        {
        }

    }
}