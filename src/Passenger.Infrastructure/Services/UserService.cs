using System;
using System.Threading.Tasks;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Converters.Interfaces;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Services.Interfaces;

namespace Passenger.Infrastructure.Services
{
  public class UserService : IUserService
  {
    private readonly IUserRepository _userRepository;
    private readonly IUserDtoConverter _userDtoConverter;

    public UserService(IUserRepository userRepository, IUserDtoConverter userDtoConverter)
    {
      _userRepository = userRepository;
      _userDtoConverter = userDtoConverter;
    }

    public async Task<UserDto> GetAsync(string email)
    {
      var user = await _userRepository.GetByEmailAsync(email);

      if(user == null)
      {
        throw new Exception($"User with email [{email}] doesn't exists");
      }

      return _userDtoConverter.Convert(user);
    }

    public async Task RegisterAsync(string email, string username, string password)
    {
      var user = await _userRepository.GetByEmailAsync(email);
      if(user != null)
      {
        throw new Exception($"User with email [{email}] already exists.");
      }

      var salt = Guid.NewGuid().ToString("N");
      user = User.Create(email, username, password, salt);
      await _userRepository.AddAsync(user);
    }
  }
}