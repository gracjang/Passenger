using Passenger.Core.Domain;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Converters.Interfaces
{
  public interface IUserDtoConverter
  {
    UserDto Convert(User user);
  }
}