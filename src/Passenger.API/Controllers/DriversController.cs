using System;
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
    public class DriversController : ApiControllerBase
    {
        private readonly IDriverService _driverService;
        private readonly IMemoryCache _cache;

        public DriversController(IMemoryCache cache, ICommandDispatcher commandDispatcher, IDriverService driverService)
            : base(commandDispatcher)
        {
            _cache = cache;
            _driverService = driverService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDrivers()
        {
            var user = await _driverService.GetAll();
            if (user == null)
            {
                return NotFound();
            }

            return Json(user);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            var driver = await _driverService.GetById(userId);
            if(driver == null)
            {
                return NotFound();
            }

            return Json(driver);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] AddDriverCommand command)
        {
            await DispatchAsync(command);

            return CreatedAtAction(nameof(Get), new { userId = command.UserId });
        }

        [HttpDelete("me")]
        [Authorize]
        public async Task<IActionResult> Delete()
        {
            await DispatchAsync(new RemoveDriverCommand());

            return NoContent();
        }

        [HttpPut("me")]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] UpdateDriverCommand command)
        {
            await DispatchAsync(command);

            return NoContent();
        }
    }
}