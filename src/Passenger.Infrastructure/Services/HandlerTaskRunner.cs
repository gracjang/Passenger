using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Passenger.Infrastructure.Services.Interfaces;

namespace Passenger.Infrastructure.Services
{
  public class HandlerTaskRunner : IHandlerTaskRunner
  {
    private readonly IHandlerService _handlerService;
    private readonly Func<Task> _validate;
    private readonly ISet<IHandlerTask> _handlerTasks;

    public HandlerTaskRunner(IHandlerService handlerService, Func<Task> validate, ISet<IHandlerTask> handlerTasks)
    {
      _handlerService = handlerService;
      _validate = validate;
      _handlerTasks = handlerTasks;
    }

    
    public IHandlerTask Run(Func<Task> run)
    {
      var handlerTask = new HandlerTask(_handlerService, run);
      _handlerTasks.Add(handlerTask);

      return handlerTask;
    }
  }
}