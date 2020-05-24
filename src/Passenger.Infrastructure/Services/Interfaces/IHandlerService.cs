using System;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services.Interfaces
{
  public interface IHandlerService
  {  
    IHandlerTask Run(Func<Task> run);

    IHandlerTaskRunner Validate(Func<Task> validate);

    Task ExecuteAllAsync();
  }
}