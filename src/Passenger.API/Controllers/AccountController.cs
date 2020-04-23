using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.User;
using Passenger.Infrastructure.Services.Interfaces;

namespace Passenger.API.Controllers
{
  [Route("api/[controller]")]
  public class AccountController : Controller
  {
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IJwtService _jwtService;

    public AccountController(ICommandDispatcher commandDispatcher, IJwtService jwtService)
    {
      _commandDispatcher = commandDispatcher;
      _jwtService = jwtService;
    }

    [HttpGet]
    [Route("token")]
    public IActionResult Get()
    {
      var token =_jwtService.CreateToken("test@22test.pl", "admin");

      return Json(token);
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