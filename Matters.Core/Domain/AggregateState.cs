using System;
using System.Collections.Generic;

namespace Matters.Core.Domain
{
    public class AggregateState : IEventSourced
    {
        public Guid Id { get; private set; }
        public int Version { get; internal set; }

        private readonly List<Event> _changes = new List<Event>();
       
        public void LoadFromHistory(IEnumerable<Event> events)
        {
            foreach (var e in events)
            {
                Apply(e);
            }
        }

        public IEnumerable<Event> GetUncommittedEvents()
        {
            return _changes;
        }

        public void MarkChangesAsCommited()
        {
            _changes.Clear();
        }

        public void Apply(Event @event)
        {
            FindApply.InvokeEvent(this, @event);
        }
    }
}
