using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WpCheckIn.Infrastructure.Exceptions
{
    public class CheckInException : Exception
    {
        public CheckInException()
        {
        }

        public CheckInException(string message) : base(message)
        {
        }

        public CheckInException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CheckInException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
