using System;
namespace Matters.Core.Domain
{
    public abstract class Event : IEvent
    {
        public Guid SourceId { get; protected set; }
        public int Version { get; protected set; }
        public DateTime Timestamp { get; protected set; }
    }
}
