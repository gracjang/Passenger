using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Driver;
using Passenger.Infrastructure.Services.Interfaces;

namespace Passenger.API.Controllers
{
    [Route("api/[controller]")]
    public class DriversController : Controller
    {
        private readonly IDriverService _driverService;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IMemoryCache _cache;

        public DriversController(IMemoryCache cache, ICommandDispatcher commandDispatcher, IDriverService driverService)
        {
            _cache = cache;
            _commandDispatcher = commandDispatcher;
            _driverService = driverService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllDrivers()
        {
            var user = await _driverService.GetAll();
            if (user == null)
            {
                return NotFound();
            }

            return Json(user);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] AddDriverCommand command)
        {
            await _commandDispatcher.DispatchAsync(command);

            return Created($"api/drivers/{command.UserId}", new object());
        }
    }
}