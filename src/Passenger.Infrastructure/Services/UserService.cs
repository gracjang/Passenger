using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Exceptions;
using Passenger.Infrastructure.Services.Interfaces;

namespace Passenger.Infrastructure.Services
{
  public class UserService : IUserService
  {
    private readonly IUserRepository _userRepository;
    private readonly IEncryptionService _encryptionService;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public UserService(
      IUserRepository userRepository,
      IEncryptionService encryptionService,
      ILogger<UserService> logger,
      IMapper mapper)
    {
      _userRepository = userRepository;
      _encryptionService = encryptionService;
      _logger = logger;
      _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> BrowseAsync()
    {
      var users = await _userRepository.GetAllAsync();

      return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto> GetAsync(string email)
    {
      var user = await _userRepository.GetByEmailAsync(email);

      if(user == null)
      {
        throw new ServiceException(ErrorCodes.UserNotFound, $"User with email [{email}] doesn't exists");
      }

      return _mapper.Map<UserDto>(user);
    }

    public async Task RegisterAsync(Guid userId, string email, string username, string password, string role)
    {
      var user = await _userRepository.GetByEmailAsync(email);
      if(user != null)
      {
        throw new ServiceException(ErrorCodes.EmailInUse, $"User with email [{email}] already exists.");
      }

      var salt = _encryptionService.GetSalt(password);
      var hashPassword = _encryptionService.GetHashPassword(password, salt);
      user = User.Create(userId, email, username, hashPassword, salt, role);

      await _userRepository.AddAsync(user);

      _logger.LogDebug($"User registered successfully. ID [{user.Id}]");
    }

    public async Task LoginAsync(string email, string password)
    {
      var user = await _userRepository.GetByEmailAsync(email);

      if(user == null)
      {
        throw new ServiceException(ErrorCodes.InvalidCredentials, "Invalid credentials");
      }

      var hash = _encryptionService.GetHashPassword(password, user.Salt);
      if(user.Password != hash)
      {
        throw new ServiceException(ErrorCodes.InvalidCredentials, "Invalid credentials");
      }

      _logger.LogDebug($"User logged successfully. ID [{user.Id}]");
    }
  }
}