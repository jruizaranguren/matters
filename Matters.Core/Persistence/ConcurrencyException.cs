using System;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Matters.Core.Persistence
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class ConcurrencyException : PersistenceBaseException
    {
        public ConcurrencyException()
        {
        }

        public ConcurrencyException(string message)
            : base(message)
        {
        }

        public ConcurrencyException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ConcurrencyException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
