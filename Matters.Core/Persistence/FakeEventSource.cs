using System;
using System.Collections.Generic;
using System.Linq;
using Matters.Core.Domain;

namespace Matters.Core.Persistence
{
    public class FakeEventSource : IEventSource
    {
        IEnumerable<Event> _uncommitedEvents = Enumerable.Empty<Event>();
        IEnumerable<Event> _commitedEvents = Enumerable.Empty<Event>();


        public FakeEventSource(IEnumerable<Event> events)
        {
            _commitedEvents = events;
        }

        public IEnumerable<Event> GetUncommitedEvents()
        {
            return _uncommitedEvents;
        }

        public void SaveEvents(Guid eventSourcedId, IEnumerable<Event> events, int expectedVersion)
        {
            _uncommitedEvents = events;
        }

        public List<Event> GetEvents(Guid eventSourcedId)
        {
            return _commitedEvents.ToList();
        }
    }
}

