using System;
using System.Runtime.Serialization;

namespace Banking.Application.Exceptions
{
    public class MoneyNotEnoughException : Exception
    {
        public MoneyNotEnoughException()
        {
        }

        public MoneyNotEnoughException(string message) : base(message)
        {
        }

        public MoneyNotEnoughException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MoneyNotEnoughException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
