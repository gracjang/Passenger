namespace Passenger.Infrastructure.Commands.User
{
  public class ChangeUserPasswordCommand : ICommand
  {
    public string CurrentPassword { get; set; }

    public string NewPassword { get; set; }
  }
}