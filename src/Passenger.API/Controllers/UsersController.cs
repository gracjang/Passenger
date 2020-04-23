using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.User;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Services.Interfaces;

namespace Passenger.API.Controllers
{
  [Route("api/[controller]")]
  public class UsersController : Controller
  {
    private readonly IUserService _userService;
    private readonly ICommandDispatcher _commandDispatcher;

    public UsersController(IUserService userService, ICommandDispatcher commandDispatcher)
    {
      _userService = userService;
      _commandDispatcher = commandDispatcher;
    }

    [HttpGet("{email}")]
    [Authorize]
    public async Task<IActionResult> Get(string email)
    {
      var user = await _userService.GetAsync(email);
      if(user == null)
      {
        return NotFound();
      }

      return Json(user);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddUserCommand command)
    {
      await _commandDispatcher.DispatchAsync(command);

      return Created($"api/users/{command.Email}", null);
    }
  }
}