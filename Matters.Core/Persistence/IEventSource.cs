using System;
using System.Collections.Generic;
using Matters.Core.Domain;

namespace Matters.Core.Persistence
{
    public interface IEventSource
    {
        void SaveEvents(Guid eventSourcedId, IEnumerable<Event> events, int expectedVersion);
        List<Event> GetEvents(Guid eventSourcedId);
    }
}
