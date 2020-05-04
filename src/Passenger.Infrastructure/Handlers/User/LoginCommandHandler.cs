using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.User;
using Passenger.Infrastructure.Extensions;
using Passenger.Infrastructure.Services.Interfaces;

namespace Passenger.Infrastructure.Handlers.User
{
  public class LoginCommandHandler : ICommandHandler<LoginCommand>
  {
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;
    private readonly IMemoryCache _memoryCache;

    public LoginCommandHandler(IUserService userService, IJwtService jwtService, IMemoryCache memoryCache)
    {
      _userService = userService;
      _jwtService = jwtService;
      _memoryCache = memoryCache;
    }

    public async Task HandleAsync(LoginCommand command)
    {
      command.TokenId = Guid.NewGuid();
      await _userService.LoginAsync(command.Email, command.Password);
      var user = await _userService.GetAsync(command.Email);
      var jwt = _jwtService.CreateToken(user.Id, user.Role);

      _memoryCache.SetJwt(command.TokenId, jwt);
    }
  }
}