using System.Threading.Tasks;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.User;
using Passenger.Infrastructure.Services.Interfaces;

namespace Passenger.Infrastructure.Handlers.User
{
  public class AddUserCommandHandler : ICommandHandler<AddUserCommand>
  {
    private readonly IUserService _userService;

    public AddUserCommandHandler(IUserService userService)
    {
      _userService = userService;
    }

    public async Task HandleAsync(AddUserCommand command)
    {
      await _userService.RegisterAsync(command.Email, command.Username, command.Password, command.Role);
    }
  }
}