using System;
using System.Threading.Tasks;
using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Extensions;
using Passenger.Infrastructure.Services.Interfaces;

namespace Passenger.Infrastructure.Services
{
  public class DriverRouteService : IDriverRouteService
  {
    private readonly IDriverRepository _driverRepository;
    private readonly IMapper _mapper;

    public DriverRouteService(IMapper mapper, IDriverRepository driverRepository)
    {
      _mapper = mapper;
      _driverRepository = driverRepository;
    }

    public async Task AddAsync(Guid userId, string name, double startLatitude, double startLongitude,
      double endLatitude, double endLongitude, double distance)
    {
      var driver = await _driverRepository.GetOrFailAsync(userId);

      var start = Node.Create("StartAddress", startLongitude, startLatitude);
      var end = Node.Create("EndAddress", endLongitude, endLatitude);
      driver.AddRoute(name, start, end, distance);

      await _driverRepository.UpdateAsync(driver);
    }

    public async Task DeleteAsync(Guid userId, string name)
    {
      var driver = await _driverRepository.GetOrFailAsync(userId);

      driver.DeleteRoute(name);
      await _driverRepository.UpdateAsync(driver);
    }
  }
}