using System;
using System.Collections.Generic;

namespace Matters.Core.Domain
{
    public class AggregateState : IEventSourced
    {
        public Guid Id { get; private set; }
       
        public void LoadFromHistory(IEnumerable<Event> events)
        {
            foreach (var e in events)
            {
                Apply(e);
            }
        }

        public IEnumerable<Event> GetUncommittedEvents()
        {
            throw new NotImplementedException();
        }

        public void MarkChangesAsCommited()
        {
            throw new NotImplementedException();
        }

        public void Apply(Event @event)
        {
            //RedirectToWhen
            throw new NotImplementedException();
        }
    }
}
