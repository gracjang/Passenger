using Microsoft.Extensions.DependencyInjection;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Repositories;

namespace Passenger.Infrastructure.IoC
{
  public static class ConfigureDependencies
  {
    public static void InstallDependencies(IServiceCollection service)
    {
      service.AddScoped<IUserRepository, UserRepository>();
    }

  }
}