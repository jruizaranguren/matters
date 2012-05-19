using System;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Matters.Core.Persistence
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class PersistenceBaseException : Exception
    {
        public PersistenceBaseException()
        {
        }

        public PersistenceBaseException(string message)
            : base(message)
        {
        }

        public PersistenceBaseException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected PersistenceBaseException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
