using Matters.Core.Domain;
using System.Collections.Generic;

namespace Matters.Core.Tests.Domain
{
    public class ProcessInstance : IAggregateRoot
    {
        ProcessInstanceState _state;



    }

    public class ProcessInstanceState : AggregateState
    {

        public ProcessInstanceState(IEnumerable<Event> events)
        {
            foreach (var @event in events)
            {
                Apply(@event);
            }
        }
    }
}
