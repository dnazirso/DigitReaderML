using System;
using System.Runtime.Serialization;

namespace Algebra.Exceptions
{
    [Serializable()]
    public class DifferentSizesException : Exception
    {
        public DifferentSizesException() : base() { }
        public DifferentSizesException(string message) : base(message) { }
        public DifferentSizesException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected DifferentSizesException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
