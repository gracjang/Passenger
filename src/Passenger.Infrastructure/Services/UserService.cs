using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
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
    private readonly IEncryptionService _encryptionService;
    private readonly ILogger _logger;

    public UserService(IUserRepository userRepository, 
      IUserDtoConverter userDtoConverter, 
      IEncryptionService encryptionService,
      ILogger<UserService> logger)
    {
      _userRepository = userRepository;
      _userDtoConverter = userDtoConverter;
      _encryptionService = encryptionService;
      _logger = logger;
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

    public async Task RegisterAsync(string email, string username, string password, string role)
    {
      var user = await _userRepository.GetByEmailAsync(email);
      if(user != null)
      {
        throw new Exception($"User with email [{email}] already exists.");
      }

      var salt = _encryptionService.GetSalt(password);
      var hashPassword = _encryptionService.GetHashPassword(password, salt);
      user = User.Create(email, username, hashPassword, salt, role);

      await _userRepository.AddAsync(user);

      _logger.LogDebug($"User registered successfully. ID [{user.Id}]");
    }

    public async Task LoginAsync(string email, string password)
    {
      var user = await _userRepository.GetByEmailAsync(email);

      if (user == null)
      {
        throw new Exception($"User with email [{email}] doesn't exists");
      }

      var hash = _encryptionService.GetHashPassword(password, user.Salt);
      if(user.Password != hash)
      {
        throw new Exception("Invalid credentials");
      }

      _logger.LogDebug($"User logged successfully. ID [{user.Id}]");
    }
  }
}