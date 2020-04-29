using System.Collections.Generic;
using System.Threading.Tasks;
using Passenger.Core.Domain;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Providers.Interfaces
{
    public interface IVehicleProvider
    {
        Task<IEnumerable<VehicleDto>> BrowseAsync();
        Task<VehicleDto> GetAsync(string brand, string name);
    }
}