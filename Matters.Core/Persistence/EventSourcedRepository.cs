using System;
using Matters.Core.Domain;

namespace Matters.Core.Persistence
{
    public class EventSourcedRepository<T> : IRepository<T> where T : IEventSourced
    {
        public static readonly int NewElementVersion = 0;
        
        private IConstructEventSourced<T> _hydrator;
        private IEventSource _source;

        public EventSourcedRepository(IConstructEventSourced<T> hydrator, IEventSource source)
        {
            _hydrator = hydrator;
            _source = source;
        }

        public void Save(T eventSourced, int expectedVersion)
        {
            _source.SaveEvents(eventSourced.Id, eventSourced.GetUncommittedEvents(), expectedVersion);
        }

        public T GetById(Guid id)
        {
            var events = _source.GetEvents(id);
            return _hydrator.Build(events);
        }
    }
}
