using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Driver, DriverDto>();
            CreateMap<Driver, DriverDetailsDto>();
            CreateMap<Node, NodeDto>();
            CreateMap<Route, RouteDto>();
            CreateMap<User, UserDto>();
            CreateMap<Vehicle, VehicleDto>();
        }
    }
}