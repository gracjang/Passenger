using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Mongo;

namespace Passenger.Infrastructure.Repositories
{
  public class UserRepository : IUserRepository
  {
    private const string UserCollectionName = "Users";
    private readonly IMongoDatabase _database;

    public UserRepository(IMongoDatabase database)
    {
      _database = database;
    }

    public async Task AddAsync(User user)
      => await Users.InsertOneAsync(user);

    public async Task<IEnumerable<User>> GetAllAsync()
      => await Users.AsQueryable().ToListAsync();

    public async Task<User> GetByEmailAsync(string email)
      => await Users.AsQueryable().FirstOrDefaultAsync(x => x.Email == email);

    public async Task<User> GetByIdAsync(Guid id)
      => await Users.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

    public async Task RemoveAsync(Guid id)
      => await Users.DeleteOneAsync(x => x.Id == id);

    public async Task UpdateAsync(User user)
      => await Users.ReplaceOneAsync(x => x.Id == user.Id, user);

    private IMongoCollection<User> Users => _database.GetCollection<User>(UserCollectionName);
  }
}