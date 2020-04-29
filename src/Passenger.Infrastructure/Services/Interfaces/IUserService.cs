using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services.Interfaces 
{
  public interface IUserService 
  {
    Task<IEnumerable<UserDto>> BrowseAsync();
    Task<UserDto> GetAsync(string email);
    Task RegisterAsync(Guid userId, string email, string username, string password, string role);
    Task LoginAsync(string email, string password);
  }
}