using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;

namespace Passenger.API.Controllers
{
    public abstract class ApiControllerBase : Controller
    {
        private readonly ICommandDispatcher _dispatcher;
        protected Guid UserId => User?.Identity?.IsAuthenticated == true ?
            Guid.Parse(User.Identity.Name) :
            Guid.Empty;

        protected ApiControllerBase(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        protected async Task DispatchAsync<T>(T command) where T : ICommand
        {
            if(command is IAuthenticationCommand authenticationCommand)
            {
                authenticationCommand.UserId = UserId;
            }

            await _dispatcher.DispatchAsync(command);
        }
    }
}