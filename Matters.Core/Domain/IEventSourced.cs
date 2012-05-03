using System;
using System.Collections.Generic;
namespace Matters.Core.Domain
{
    public interface IEventSourced : IHaveIdentity<Guid>
    {
        void Apply(Event @event, bool isNew);
        void LoadFromHistory(IEnumerable<Event> events);
        IEnumerable<Event> GetUncommittedEvents();
        void MarkChangesAsCommited();
    }
}
