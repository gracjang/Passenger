using Passenger.Infrastructure.Commands;
using System.Threading.Tasks;
using Passenger.Infrastructure.Commands.User;
using Passenger.Infrastructure.Services.Interfaces;

namespace Passenger.Infrastructure.Handlers.User
{
  public class ChangeUserPasswordCommandHandler : ICommandHandler<ChangeUserPasswordCommand>
  {
    private readonly IUserService _userService;

    public ChangeUserPasswordCommandHandler(IUserService userService)
    {
      _userService = userService;
    }

    public Task HandleAsync(ChangeUserPasswordCommand command)
    {
      throw new System.NotImplementedException();
    }
  }
}