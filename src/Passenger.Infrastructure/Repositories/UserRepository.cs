using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;

namespace Passenger.Infrastructure.Repositories
{
  public class UserRepository : IUserRepository
  {
    private static readonly ISet<User> _users = new HashSet<User>();

    public UserRepository()
    {
    }

    public async Task AddAsync(User user)
    {
      _users.Add(user);
      await Task.CompletedTask;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
      return await Task.FromResult(_users);
    }

    public async Task<User> GetByEmailAsync(string email)
    {
      return await Task.FromResult(_users.SingleOrDefault(x => x.Email == email));
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
      return await Task.FromResult(_users.SingleOrDefault(x => x.Id == id));
    }

    public async Task RemoveAsync(Guid id)
    {
      var user = await GetByIdAsync(id);
      _users.Remove(user);
      await Task.CompletedTask;
    }

    public async Task UpdateAsync(User user)
    {
      await Task.CompletedTask;
    }
  }
}