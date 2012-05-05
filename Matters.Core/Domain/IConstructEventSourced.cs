using System.Collections.Generic;

namespace Matters.Core.Domain
{
    public interface IConstructEventSourced<TEventSourced> where TEventSourced : IEventSourced
    {
        TEventSourced Build(IEnumerable<Event> events);
    }
}
