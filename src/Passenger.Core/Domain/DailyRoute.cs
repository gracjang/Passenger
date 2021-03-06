using System;
using System.Collections.Generic;
using System.Linq;

namespace Passenger.Core.Domain
{
  public class DailyRoute
  {
    private ISet<PassengerNode> _passengerNodes = new HashSet<PassengerNode>();

    protected DailyRoute()
    {
    }

    public DailyRoute(Route route)
    {
      Id = Guid.NewGuid();
      SetRoute(route);
    }

    public Guid Id { get; protected set; }
    public Route Route { get; protected set; }

    public IEnumerable<PassengerNode> PassengerNodes
    {
      get => _passengerNodes;
      set => _passengerNodes = new HashSet<PassengerNode>();
    }

    public void SetRoute(Route route)
    {
      if(Route == route) return;

      Route = route;
    }

    public void AddPassengerNode(Passenger passenger, Node node)
    {
      var passengerNode = GetPassengerNode(passenger);
      if(passengerNode != null)
        throw new InvalidOperationException($"Node already exists for passenger: {passenger.UserId}.");

      _passengerNodes.Add(PassengerNode.Create(node, passenger));
    }

    public void RemovePassengerNode(Passenger passenger)
    {
      var passengerNode = GetPassengerNode(passenger);
      if(passenger == null) return;

      _passengerNodes.Remove(passengerNode);
    }

    private PassengerNode GetPassengerNode(Passenger passenger)
    {
      return PassengerNodes.SingleOrDefault(x => x.Passenger.UserId == passenger.UserId);
    }
  }
}