using System;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services.Interfaces
{
  public interface IHandlerTaskRunner
  {
    IHandlerTask Run(Func<Task> run);
  }
}