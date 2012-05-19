using System;
using System.Collections.Generic;
using System.Linq;

namespace Matters.Core.Domain
{
    public abstract class AggregateState<T> : IAggregateState where T: AggregateState<T>
    {
        public Guid Id { get; protected set; }
        public int Version { get; protected set; }

        private readonly List<Event> _changes = new List<Event>();
       
        public void LoadFromHistory(IEnumerable<Event> events)
        {
            foreach (var e in events)
            {
                Apply(e,false);
            }
            Version = events.Count() > 0 ? events.Last().Version : 0;
        }

        public IEnumerable<Event> GetUncommittedEvents()
        {
            return _changes;
        }

        public void MarkChangesAsCommited()
        {
            _changes.Clear();
        }

        public void Apply(Event @event, bool isNew)
        {
           
            FindApply.InvokeEvent(this as T, @event);
            if (isNew)
            {
                Version +=1;
                @event.SetActualVersion(Version);
                _changes.Add(@event);
            }
        }
    }
}
