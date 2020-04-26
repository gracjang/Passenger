using System.Collections.Generic;
using Passenger.Core.Domain;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Converters.Interfaces
{
  public interface IDriverDtoConverter
  {
    DriverDto Convert(Driver driver);

    IEnumerable<DriverDto> Convert(IEnumerable<Driver> drivers);
  }
}