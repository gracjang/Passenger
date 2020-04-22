using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.User;

namespace Passenger.API.Controllers
{
  [Route("api/[controller]")]
  public class AccountController : Controller
  {
    private readonly ICommandDispatcher _commandDispatcher;
    public AccountController(ICommandDispatcher commandDispatcher)
    {
      _commandDispatcher = commandDispatcher;
    }

    [HttpPut]
    [Route("password")]
    public async Task<IActionResult> ChangeUserPassword([FromBody] ChangeUserPasswordCommand command)
    {
      await _commandDispatcher.DispatchAsync(command);

      return NoContent();
    }
  }
}