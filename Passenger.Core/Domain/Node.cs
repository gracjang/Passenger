using System;
using System.Text.RegularExpressions;
using Passenger.Core.Exceptions;

namespace Passenger.Core.Domain
{
    public class Node
    {
        public string Address { get; protected set; }
        public double Longitude {get; protected set; }
        public double Latitude { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        protected Node()
        {
        }

        protected Node(string address, double longitude, double latitude)
        {
            SetAddress(address);
            SetLongitude(longitude);
            SetLatitude(latitude);
        }

        public static void Create(string address, double longitude, double latitude)
            => new Node(address, longitude, latitude);

        private void SetAddress(string address)
        {
            if(string.IsNullOrEmpty(address))
            {
                throw new Exception("Address can't be null."); 
            }
            if(Address == address)
            {
                return;
            }
            
            Address = address;
            UpdatedAt = DateTime.UtcNow;
        }

        private void SetLongitude(double longitude)
        {
            if(double.IsNaN(longitude))
            {
                throw new Exception("Longitude must be a number."); 
            }
            if(Longitude == longitude)
            {
                return;
            }
            
            Longitude = longitude;
            UpdatedAt = DateTime.UtcNow;
        }

        private void SetLatitude(double latitude)
        {
            if(double.IsNaN(latitude))
            {
                throw new Exception("Latitude must be a number."); 
            }
            if(Latitude == latitude)
            {
                return;
            }
            
            Longitude = latitude;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}