using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Services.Interfaces;

namespace Passenger.API.Controllers
{
  [Route("api/[controller]")]
  public class UsersController : Controller
  {
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
      _userService = userService;
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
    public async Task<IActionResult> Post([FromBody] UserDto user)
    {
      await _userService.RegisterAsync(user.Email, user.Username, user.Password);

      return Created($"users/{user.Email}", null);
    }

  }
}