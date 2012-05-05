using System;
using System.Collections.Generic;
using System.Linq;
using EventStore;
using Matters.Core.Domain;

namespace Matters.Core.Persistence
{
    public class JOliverEventSource : IEventSource
    {
        private static IStoreEvents _store;

        public JOliverEventSource(IStoreEvents store)
        {
            _store = store;
        }

        public void SaveEvents(Guid eventSourcedId, IEnumerable<Event> events, int expectedVersion)
        {
            int maxRevision = Int32.MaxValue;
           
            events.ToList()
                .ForEach(x => x.OccursNow()); //set the event timestamp just before saving it

            using (IEventStream eventStream = _store.OpenStream(eventSourcedId, expectedVersion, maxRevision))
            {
                if (eventStream.CommitSequence > expectedVersion)
                {
                    throw new Persistence.ConcurrencyException();
                }

                events
                    .Cast<object>()
                    .Select(x => new EventMessage { Body = x })
                    .ToList()
                    .ForEach(eventStream.Add);

                eventStream.CommitChanges(Guid.NewGuid());
            }
        }

        public List<Event> GetEvents(Guid eventSourcedId)
        {
            int minRevision = Int32.MinValue;
            int maxRevision = Int32.MaxValue;
            List<Event> events;

            using (IEventStream eventStream = _store.OpenStream(eventSourcedId, minRevision, maxRevision))
            {
                events = eventStream.CommittedEvents
                    .Select(x => x.Body)
                    .Cast<Event>().ToList<Event>();
            }
            return events;
        }
    }
}
