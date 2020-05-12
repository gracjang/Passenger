using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Driver;
using Passenger.Infrastructure.Providers.Interfaces;
using Passenger.Infrastructure.Services.Interfaces;

namespace Passenger.API.Controllers
{
    [Route("api/[controller]")]
    public class VehiclesController : ApiControllerBase
    {
        private readonly IVehicleProvider _vehicleProvider;
        public VehiclesController(ICommandDispatcher commandDispatcher, IVehicleProvider provider)
            : base(commandDispatcher)
        {
            _vehicleProvider = provider;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var vehicles = await _vehicleProvider.BrowseAsync();
            if (vehicles == null)
            {
                return NotFound();
            }

            return Json(vehicles);
        }
    }
}