using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Passenger.Core.Repositories;

namespace Passenger.API.Controllers
{
  [Route("api/[controller]")]
  public class ValueController : Controller
  {
    private readonly IUserRepository _userRepository;

    public ValueController(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      for (int i = 0; i < 10; i++)
      {
        await _userRepository.AddAsync(Core.Domain.User.Create("test@gmail.com", $"testowy{i}", "password", "Salt"));
      }

      var response = _userRepository.GetAllAsync().Result;
      return Json(response);
    }
  }
}