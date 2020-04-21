using System;
using System.Collections.Generic;
using Passenger.Core.Domain;

namespace Passenger.Infrastructure.DTO
{
  public class DriverDto
  {
    public Guid UserId { get; protected set; }

    public Vehicle Vehicle { get; protected set; }

    public IEnumerable<Route> Routes { get; set; }

    public IEnumerable<DailyRoute> DailyRoutes { get; set; }
  }
}