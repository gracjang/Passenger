using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Infrastructure.Converters.Interfaces;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Converters
{
  public class UserDtoConverter : IUserDtoConverter
  {
    private readonly IMapper _mapper;
    public UserDtoConverter()
    {
      var configuration = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<User, UserDto>();
      });

      configuration.AssertConfigurationIsValid();
      _mapper = configuration.CreateMapper();
    }
    public UserDto Convert(User user)
    {
      return _mapper.Map<UserDto>(user);
    }
  }
}