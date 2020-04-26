using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Converters.Interfaces;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Services.Interfaces;

namespace Passenger.Infrastructure.Services 
{
    public class DriverService : IDriverService 
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;
        private readonly IDriverDtoConverter _driverDtoConverter;

        public DriverService(
            IDriverRepository driverRepository, 
            ILogger<DriverService> logger, 
            IDriverDtoConverter driverConverter, 
            IUserRepository userRepository)
        {
            _driverRepository = driverRepository;
            _logger = logger;
            _driverDtoConverter = driverConverter;
            _userRepository = userRepository;
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
            return _driverDtoConverter.Convert(drivers);
        }

        public async Task<DriverDto> GetById(Guid userId) 
        {
            var driver = await _driverRepository.GetByIdAsync(userId);
            if (driver == null)
            {
                throw new Exception($"Driver with Id [{userId}] doesn't exists.");
            }

            return _driverDtoConverter.Convert(driver);
        }

        public async Task SetVehicle(Guid userId, string brand, string name, int seats) 
        {
            var driver = await _driverRepository.GetByIdAsync(userId);
            if(driver == null)
            {
                throw new Exception($"Driver with userId: [{userId}] not found.");
            }

            var vehicle = Vehicle.Create(name, seats, brand);
            driver.SetVehicle(vehicle);
            
            await _driverRepository.UpdateAsync(driver);
        }
    }
}