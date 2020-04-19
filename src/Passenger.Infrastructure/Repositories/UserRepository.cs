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
    private readonly IMongoCollection<User> _users;
    public UserRepository(IMongoSettings mongoSettings)
    {
      var client = new MongoClient(mongoSettings.ConnectionString);
      var database = client.GetDatabase(mongoSettings.Database);

      _users = database.GetCollection<User>(mongoSettings.CollectionName);
    }

    public async Task AddAsync(User user)
      => await _users.InsertOneAsync(user);


    public async Task<IEnumerable<User>> GetAllAsync()
      => await _users.AsQueryable().ToListAsync();

    public async Task<User> GetByEmailAsync(string email)
      => await _users.AsQueryable().FirstOrDefaultAsync(x => x.Email == email);

    public async Task<User> GetByIdAsync(Guid id)
      => await _users.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

    public async Task RemoveAsync(Guid id)
      => await _users.DeleteOneAsync(x => x.Id == id);

    public async Task UpdateAsync(User user)
      => await _users.ReplaceOneAsync(x => x.Id == user.Id, user);
  }
}