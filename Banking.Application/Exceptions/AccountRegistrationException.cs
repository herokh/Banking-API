using System;
using System.Runtime.Serialization;

namespace Banking.Application.Exceptions
{
    public class AccountRegistrationException : Exception
    {
        public AccountRegistrationException()
        {
        }

        public AccountRegistrationException(string message) : base(message)
        {
        }

        public AccountRegistrationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountRegistrationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
