using System;
using System.Collections.Generic;

namespace Passenger.Core.Domain
{
  public class Passenger
  {
    public Guid Id { get; protected set; }
    public Guid UserId { get; protected set; }
    public Node Address { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }

    protected Passenger(Node address, Guid userId)
    {
      Id = Guid.NewGuid();
      UserId = userId;
      Address = address;
    }

    protected Passenger()
    {
    }

    public static Passenger Create(Node address, Guid userId)
      => new Passenger(address, userId);
  }
}