using System.Threading.Tasks;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Driver;
using Passenger.Infrastructure.Services.Interfaces;

namespace Passenger.Infrastructure.Handlers.Driver
{
  public class AddDriverCommandHandler : ICommandHandler<AddDriverCommand>
  {
    private readonly IDriverService _driverService;

    public AddDriverCommandHandler(IDriverService driverService)
    {
      _driverService = driverService;
    }

    public async Task HandleAsync(AddDriverCommand command)
    {
      await _driverService.CreateAsync(command.UserId);
      var vehicle = command.Vehicle;
      await _driverService.SetVehicle(command.UserId, vehicle.Brand, vehicle.Name);
    }
  }
}