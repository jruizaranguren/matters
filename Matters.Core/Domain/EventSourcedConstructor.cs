using System.Collections.Generic;

namespace Matters.Core.Domain
{
    public abstract class EventSourcedConstructor<T> : IConstructEventSourced<T> where T:IEventSourced
    {
        protected abstract T BuildFromHistory(IEnumerable<Event> events);

        public T Build(IEnumerable<Event> events)
        {
            return BuildFromHistory(events);
        }
    }
}
