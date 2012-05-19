using System;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Matters.Core.Persistence
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class AggregateNotFoundException : PersistenceBaseException
    {
        public AggregateNotFoundException()
        {
        }

        public AggregateNotFoundException(string message)
            : base(message)
        {
        }

        public AggregateNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected AggregateNotFoundException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
