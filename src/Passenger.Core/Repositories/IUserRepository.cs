namespace Passenger.Core.Repositories
{
    public interface IUserRepository : IRepository
    {
         Task<User> GetByEmailAsync(string email);
         Task<User> GetByIdAsync(Guid id);
         Task<IEnumerable<User>> GetAllAsync();
         Task AddAsync(User user);
         Task UpdateAsync(User user);
         Task RemoveAsync(Guid id);
    }
}