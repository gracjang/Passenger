using System;

namespace Passenger.Core.Exceptions
{
    public class NullOrEmptyException : Exception
    {
        public NullOrEmptyException()
        {
        }
        public NullOrEmptyException(string message) : base(message)
        {
        }
    }
}