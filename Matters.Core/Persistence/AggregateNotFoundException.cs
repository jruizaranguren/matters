using System;
using System.Runtime.Serialization;

namespace Matters.Core.Persistence
{
    [Serializable]
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
