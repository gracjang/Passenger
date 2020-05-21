using System;
using System.Threading.Tasks;
using Passenger.Core.Exceptions;

namespace Passenger.Infrastructure.Services.Interfaces
{
	public interface IHandlerTask
	{
		IHandlerTask Always(Func<Task> always);

		IHandlerTask OnCustomError(Func<PassengerException, Task> onCustomError,
			bool propagateException = false, bool executeOnError = false);

		IHandlerTask OnError(Func<Exception, Task> onError,
			bool propagateException = false, bool executeOnError = false);

    IHandlerTask OnSuccess(Func<Task> onSuccess);

		IHandlerTask PropagateException();

    IHandlerTask DoNotPropagateException();

		IHandlerService Next();

    Task ExecuteAsync();
  }
}