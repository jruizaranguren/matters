using Matters.Core.Domain;
using System.Collections.Generic;

namespace Matters.Core.Tests.Domain
{
    public class ProcessInstance : IAggregateRoot
    {
        ProcessInstanceState _state;

        public ProcessInstance(ProcessInstanceState state)
        {
            _state = state;
        }

        void Apply(Event @event)
        {
            _state.Apply(@event, true);
        }
    }

    public class ProcessInstanceState : AggregateState
    {

        public ProcessInstanceState(IEnumerable<Event> events)
        {
            LoadFromHistory(events);
        }
    }
}
