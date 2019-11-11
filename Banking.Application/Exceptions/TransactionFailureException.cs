using System;
using System.Runtime.Serialization;

namespace Banking.Application.Exceptions
{
    public class TransactionFailureException : Exception
    {
        public TransactionFailureException()
        {
        }

        public TransactionFailureException(string message) : base(message)
        {
        }

        public TransactionFailureException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TransactionFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
