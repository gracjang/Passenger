using System;

namespace Passenger.Infrastructure.DTO
{
    public class DriverDto
  {
    public Guid UserId { get; protected set; }

    public VehicleDto Vehicle { get; protected set; }

    public DateTime UpdatedAt { get; set; }
  }
}