using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.User;
using Passenger.Infrastructure.Extensions;
using Passenger.Infrastructure.Services.Interfaces;

namespace Passenger.API.Controllers
{
  [Route("api/[controller]")]
  public class UsersController : ApiControllerBase
  {
    private readonly IUserService _userService;
    private readonly IMemoryCache _cache;

    public UsersController(IUserService userService, ICommandDispatcher commandDispatcher, IMemoryCache cache)
      : base(commandDispatcher)
    {
      _userService = userService;
      _cache = cache;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var user = await _userService.BrowseAsync();
      if(user == null)
      {
        return NotFound();
      }

      return Json(user);
    }

    [HttpGet("{email}")]
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
    [Route("register")]
    public async Task<IActionResult> Post([FromBody] AddUserCommand command)
    {
      await DispatchAsync(command);

      return Created($"api/users/{command.Email}", new object());
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Post([FromBody] LoginCommand command)
    {
      command.TokenId = Guid.NewGuid();
      await DispatchAsync(command);

      return Json(_cache.GetJwt(command.TokenId));
    }
  }
}