using System.Threading.Tasks;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Driver;
using Passenger.Infrastructure.Services.Interfaces;

namespace Passenger.Infrastructure.Handlers.Driver
{
    public class RemoveDriverRouteCommandHandler : ICommandHandler<RemoveDriverRouteCommand>
    {
        private readonly IDriverRouteService _driverRouteService;

        public RemoveDriverRouteCommandHandler(IDriverRouteService driverRouteService)
        {
            _driverRouteService = driverRouteService;
        }
        public async Task HandleAsync(RemoveDriverRouteCommand command)
        {
           await _driverRouteService.DeleteAsync(command.UserId, command.Name);
        }
    }
}