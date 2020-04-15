using System;
using System.Collections.Generic;
using System.Linq;

namespace Passenger.Core.Domain
{
    public class Driver
    {
        private ISet<Route> _routes = new HashSet<Route>();
        public Guid UserId { get; protected set; }
        public string Name { get; set; }
        public Vehicle Vehicle { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public IEnumerable<Route> Routes 
        { 
            get { return _routes; } 
            set { _routes = new HashSet<Route>(); }
        }

        protected Driver()
        {   
        }

        public Driver(User user, Vehicle vehicle)
        {
            UserId = user.Id;
            Name = user.Username;
            SetVehicle(vehicle);
        }

        public void SetVehicle(Vehicle vehicle)
        {
            Vehicle = vehicle;
            UpdatedAt = UpdatedAt;
        }

        public void AddRoute(string name, Node start, Node end, double distance)
        {
            var route = GetRoute(name);

            if(route != null)
            {
                throw new InvalidOperationException($"Route {name} already exists for Driver: {Name}.");
            }

            _routes.Add(Route.Create(name, start, end, distance));
            UpdatedAt = UpdatedAt;
        }

        public void DeleteRoute(string name)
        {
            var route = GetRoute(name);

            if(route == null)
            {
                return;
            }

            _routes.Remove(route);
            UpdatedAt = UpdatedAt;
        }

        private Route GetRoute(string name)
            => Routes.SingleOrDefault(x => x.Name == name);

    }
}