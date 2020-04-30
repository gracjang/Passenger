using System.Threading.Tasks;
using Passenger.Core.Domain;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Driver;
using Passenger.Infrastructure.Services.Interfaces;

namespace Passenger.Infrastructure.Handlers.Driver
{
    public class AddDriverRouteCommandHandler : ICommandHandler<AddDriverRouteCommand>
    {
        private readonly IDriverRouteService _driverRouteService;

        public AddDriverRouteCommandHandler(IDriverRouteService driverRouteService)
        {
            _driverRouteService = driverRouteService;
        }

        public async Task HandleAsync(AddDriverRouteCommand command)
        {
            await _driverRouteService.AddAsync(command.UserId, command.Name, 
                command.StartLatitude, command.StartLongitude, 
                command.EndLatitude, command.EndLongitude, 
                command.Distance);
        }
    }
}