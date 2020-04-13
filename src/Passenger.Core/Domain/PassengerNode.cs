using System;

namespace Passenger.Core.Domain
{
    public class PassengerNode
    {
        public Node Node {get; protected set; }
        public Passenger Passenger {get; protected set; } 

        protected PassengerNode()
        {
        }

        protected PassengerNode(Node node, Passenger passenger)
        {
            Node = node;
            Passenger = passenger;
        }

        public static PassengerNode Create(Node node, Passenger passenger)
            => new PassengerNode(node, passenger);
    }
}