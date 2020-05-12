using System.Threading.Tasks;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Driver;
using Passenger.Infrastructure.Services.Interfaces;

namespace Passenger.Infrastructure.Handlers.Driver
{
    public class RemoveDriverCommandHandler : ICommandHandler<RemoveDriverCommand>
    {
        private readonly IDriverService _driverService;

        public RemoveDriverCommandHandler(IDriverService driverService)
        {
            _driverService = driverService;
        }

        public async Task HandleAsync(RemoveDriverCommand command)
        {
           await _driverService.DeleteAsync(command.UserId);
        }
    }
}