using System;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services.Interfaces
{
  public interface IDriverRouteService
  {
    Task AddAsync(Guid userId, string name,
      double startLatitude, double startLongitude,
      double endLatitude, double endLongitude, double distance);

    Task DeleteAsync(Guid userId, string name);
  }
}