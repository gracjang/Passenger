using System.Threading.Tasks;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Driver;
using Passenger.Infrastructure.Services.Interfaces;

namespace Passenger.Infrastructure.Handlers.Driver
{
  public class UpdateDriverCommandHandler : ICommandHandler<UpdateDriverCommand>
  {
    private readonly IDriverService _driverService;

    public UpdateDriverCommandHandler(IDriverService driverService)
    {
      _driverService = driverService;
    }

    public async Task HandleAsync(UpdateDriverCommand command)
    {
      var vehicle = command.Vehicle;
      await _driverService.SetVehicle(command.UserId, vehicle.Brand, vehicle.Name);
    }
  }
}