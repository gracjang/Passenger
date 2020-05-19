using System;
using Passenger.Core.Exceptions;

namespace Passenger.Infrastructure.Exceptions
{
    public class ServiceException : PassengerException
    {
        public ServiceException()
        {
        }

        public ServiceException(string code)
          : base(code)
        {
        }

        public ServiceException(string message, params object[] args)
          : base(message, args)
        {
        }

        public ServiceException(string code, string message, params object[] args)
          : base(code, message, args)
        {
        }

        public ServiceException(Exception innerException, string message, params object[] args)
          : base(innerException, message, args)
        {
        }

        public ServiceException(Exception innerException, string code, string message, params object[] args)
          : base(innerException, code, message, args)
        {
        }
    }
}