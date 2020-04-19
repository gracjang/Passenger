using System;
using System.Threading.Tasks;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services.Interfaces
{
  public interface IUserService
  {
    Task<UserDto> GetAsync(string email);

    Task RegisterAsync(string email, string username, string password);
  }
}