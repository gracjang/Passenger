using System.Collections.Generic;
using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Infrastructure.Converters.Interfaces;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Converters {
  public class DriverDtoConverter : IDriverDtoConverter {
    private readonly IMapper _mapper;

    public DriverDtoConverter() {
      var configuration = new MapperConfiguration(cfg => {
        cfg.CreateMap<Driver, DriverDto>()
          .ForMember(x => x.Vehicle, opt => opt.MapFrom(y => _mapper.Map<VehicleDto>(y.Vehicle)));
      });

      configuration.AssertConfigurationIsValid();
      _mapper = configuration.CreateMapper();
    }

    public DriverDto Convert(Driver driver) => _mapper.Map<DriverDto>(driver);

    public IEnumerable<DriverDto> Convert(IEnumerable<Driver> drivers)
      {
        var driversDto = new List<DriverDto>();
        foreach(var driver in drivers)
        {
          driversDto.Add(_mapper.Map<DriverDto>(driver));
        }

        return driversDto;
      }
  }
}