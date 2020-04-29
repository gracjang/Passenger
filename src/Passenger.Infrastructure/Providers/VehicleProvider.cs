using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Providers.Interfaces;

namespace Passenger.Infrastructure.Providers
{
    public class VehicleProvider : IVehicleProvider
    {
       private static readonly IDictionary<string, IEnumerable<VehicleDetails>> availableVehicles = 
            new Dictionary<string, IEnumerable<VehicleDetails>>
        {
            ["Audi"] = new List<VehicleDetails>(),
            ["BMW"] = new List<VehicleDetails>
            {
                new VehicleDetails("i8", 3),
                new VehicleDetails("E36", 5)
            },
            ["Ford"] = new List<VehicleDetails>
            {
                new VehicleDetails("Fiesta", 5),
            },
            ["Skoda"] = new List<VehicleDetails>
            {
                new VehicleDetails("Fabia", 5),
                new VehicleDetails("Octavia", 5)
            },
            ["Volkswagen"] = new List<VehicleDetails>()
        };

        private readonly static string CacheKey = "vehicles";
        private readonly IMemoryCache _cache;

        public VehicleProvider(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<IEnumerable<VehicleDto>> BrowseAsync()
        {
            var vehicles =  _cache.Get<IEnumerable<VehicleDto>>(CacheKey);
            if(vehicles == null)
            {
                vehicles = await GetAllAsync();
                _cache.Set(CacheKey, vehicles);
            }

            return vehicles; 
        }
                
        public async Task<VehicleDto> GetAsync(string brand, string name)
        {
            if(!availableVehicles.ContainsKey(brand))
            {
                throw new Exception($"Vehicle brand: '{brand}' is not available.");
            }
            var vehicles = availableVehicles[brand];
            var vehicle = vehicles.SingleOrDefault(x => x.Name == name);
            if(vehicle == null)
            {
                throw new Exception($"Vehicle: '{name}' for brand: '{brand}' is not available.");
            }

            return await Task.FromResult(new VehicleDto
            {
                Brand = brand,
                Name = vehicle.Name,
                Seats = vehicle.Seats
            });
        }
        
        private async Task<IEnumerable<VehicleDto>> GetAllAsync() 
            => await Task.FromResult(availableVehicles.GroupBy(x => x.Key)
                .SelectMany(g => g.SelectMany(v => v.Value.Select(x => new VehicleDto
                {
                    Brand = v.Key,
                    Name = x.Name,
                    Seats = x.Seats
                }))));

        private class VehicleDetails
        {
            public string Name { get; }
            public int Seats { get; }

            public VehicleDetails(string name, int seats)
            {
                Name = name;
                Seats = seats;
            } 
        }
    }
}