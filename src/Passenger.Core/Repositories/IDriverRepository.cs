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