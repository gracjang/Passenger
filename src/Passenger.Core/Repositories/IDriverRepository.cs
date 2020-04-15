using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Passenger.Core.Domain;

namespace Passenger.Core.Repositories
{
    public interface IDriverRepository : IRepository
    {
         Task<Driver> GetByIdAsync(Guid userId);
         Task<IEnumerable<Driver>> GetAllAsync();
         Task AddAsync(Driver user);
         Task UpdateAsync(Driver user);
         Task RemoveAsync(Driver id);
    }
}