using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Infrastructure.Converters.Interfaces;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Converters
{
  public class DriverDtoConverter : IDriverDtoConverter
  {
    private readonly IMapper _mapper;

    public DriverDtoConverter()
    {
      var configuration = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<Driver, DriverDto>();
      });

      configuration.AssertConfigurationIsValid();
      _mapper = configuration.CreateMapper();
    }

    public DriverDto Convert(Driver driver)
      => _mapper.Map<DriverDto>(driver);
  }
}