using System;
using System.Threading.Tasks;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;

namespace Passenger.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<Driver> GetOrFailAsync(this IDriverRepository repository, Guid userId)
        {
            var driver = await repository.GetByIdAsync(userId);
            if(driver == null)
            {
                throw new Exception($"Driver with userId: [{userId}] not found.");
            }

            return driver;
        }

        public static async Task<User> GetOrFailAsync(this IUserRepository repository, Guid userId)
        {
            var user = await repository.GetByIdAsync(userId);
            if(user == null)
            {
                throw new Exception($"User with id: [{userId}] not found.");
            }

            return user;
        }
    }
}