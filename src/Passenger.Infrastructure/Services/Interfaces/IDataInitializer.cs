using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services.Interfaces
{
    public interface IDataInitializer
    {
         Task SeedAsync();
    }
}