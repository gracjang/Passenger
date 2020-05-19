using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services.Interfaces
{
  public interface IDriverService
  {
    Task<DriverDetailsDto> GetById(Guid userId);

    Task<IEnumerable<DriverDto>> GetAll();

    Task CreateAsync(Guid userId);

    Task SetVehicle(Guid userId, string brand, string name);

    Task DeleteAsync(Guid userId);
  }
}