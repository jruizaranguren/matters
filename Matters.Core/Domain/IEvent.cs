using System;
namespace Matters.Core.Domain
{
    public interface IEvent
    {
        Guid SourceId { get; }
        int Version { get; }
        DateTime Timestamp { get; }
    }
}
