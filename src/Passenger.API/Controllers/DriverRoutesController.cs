using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Driver;

namespace Passenger.API.Controllers
{
  [Route("driver/routes")]
  public class DriverRoutesController : ApiControllerBase
  {
    public DriverRoutesController(ICommandDispatcher commandDispatcher)
      : base(commandDispatcher)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddDriverRouteCommand command)
    {
      await DispatchAsync(command);

      return NoContent();
    }


    [HttpDelete("name")]
    public async Task<IActionResult> Delete(string name)
    {
      var command = new RemoveDriverRouteCommand
      {
        Name = name,
      };

      await DispatchAsync(command);

      return NoContent();
    }
  }
}