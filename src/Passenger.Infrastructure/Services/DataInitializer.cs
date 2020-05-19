using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Passenger.Infrastructure.Services.Interfaces;

namespace Passenger.Infrastructure.Services
{
  public class DataInitializer : IDataInitializer
  {
    private readonly IUserService _userService;
    private readonly IDriverService _driverService;
    private readonly IDriverRouteService _driverRouteService;
    private readonly ILogger _logger;

    public DataInitializer(
      IUserService userService,
      IDriverService driverService,
      IDriverRouteService driverRouteService,
      ILogger<DataInitializer> logger)
    {
      _userService = userService;
      _driverService = driverService;
      _driverRouteService = driverRouteService;
      _logger = logger;
    }

    public async Task SeedAsync()
    {
      var users = await _userService.BrowseAsync();
      if(users.Any())
      {
        _logger.LogInformation("Data was already initialized.");

        return;
      }

      _logger.LogInformation("Initializing data...");
      var tasks = new List<Task>();
      for (var i = 1; i <= 10; i++)
      {
        var userId = Guid.NewGuid();
        var username = $"user{i}";
        await _userService.RegisterAsync(userId, $"user{i}@test.com",
          username, "secret", "user");
        _logger.LogInformation($"Adding user: '{username}'.");
        await _driverService.CreateAsync(userId);
        await _driverService.SetVehicle(userId, "BMW", "i8");
        await _driverRouteService.AddAsync(userId, "Default route",
          1, 1, 2, 2, 25);
        await _driverRouteService.AddAsync(userId, "Job route",
          3, 3, 5, 5, 10);
        _logger.LogInformation($"Adding driver for: '{username}'.");
      }

      for (var i = 1; i <= 3; i++)
      {
        var userId = Guid.NewGuid();
        var username = $"admin{i}";
        _logger.LogInformation($"Adding admin: '{username}'.");
        await _userService.RegisterAsync(userId, $"admin{i}@test.com",
          username, "secret", "admin");
      }

      _logger.LogInformation("Data was initialized.");
    }
  }
}