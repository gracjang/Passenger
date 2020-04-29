using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Providers.Interfaces;
using Passenger.Infrastructure.Services.Interfaces;

namespace Passenger.Infrastructure.Services
{
    public class DriverService : IDriverService 
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IVehicleProvider _vehicleProvider;

        public DriverService(
            IDriverRepository driverRepository,
            ILogger<DriverService> logger,
            IUserRepository userRepository,
            IVehicleProvider vehicleProvider,
            IMapper mapper)
        {
            _driverRepository = driverRepository;
            _logger = logger;
            _userRepository = userRepository;
            _vehicleProvider = vehicleProvider;
            _mapper = mapper;
        }

        public async Task CreateAsync(Guid userId) 
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if(user == null)
            {
                throw new Exception($"User with Id: [{userId}] not found.");
            }

            var driver = await _driverRepository.GetByIdAsync(userId);
            if(driver != null)
            {
                throw new Exception($"Driver with Id: [{userId}] already exists.");
            }
            driver = new Driver(user);
            await _driverRepository.AddAsync(driver);
        }

        public async Task<IEnumerable<DriverDto>> GetAll()
        {
            var drivers = await _driverRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<Driver>, IEnumerable<DriverDto>>(drivers);
        }

        public async Task<DriverDetailsDto> GetById(Guid userId) 
        {
            var driver = await _driverRepository.GetByIdAsync(userId);
            if (driver == null)
            {
                throw new Exception($"Driver with Id [{userId}] doesn't exists.");
            }

            return _mapper.Map<DriverDetailsDto>(driver);
        }

        public async Task SetVehicle(Guid userId, string brand, string name) 
        {
            var driver = await _driverRepository.GetByIdAsync(userId);
            if(driver == null)
            {
                throw new Exception($"Driver with userId: [{userId}] not found.");
            }

            var vehicleDto = await _vehicleProvider.GetAsync(brand, name);
            var vehicle = Vehicle.Create(name, vehicleDto.Seats, brand);
            driver.SetVehicle(vehicle);
            
            await _driverRepository.UpdateAsync(driver);
        }
    }
}