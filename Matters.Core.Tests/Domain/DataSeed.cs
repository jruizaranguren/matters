using Matters.Core.Domain;
using Matters.Core.Persistence;
using System.Collections.Generic;
using System;

namespace Matters.Core.Tests.Domain
{
    #region ProcessInstanceAggregate
    public class ProcessInstance : IAggregateRoot
    {
        ProcessInstanceState _state;
        public ProcessInstance(ProcessInstanceState state)
        {
            _state = state; 
        }

        protected void Apply(Event @event)
        {
            _state.Apply(@event, true);
        }
    }

    public class ProcessInstanceState : AggregateState<ProcessInstanceState>
    {

        public ProcessInstanceState(IEnumerable<Event> events)
        {
            LoadFromHistory(events);
        }
    }

    public class ProcessInstanceFactory : EventSourcedConstructor<ProcessInstanceState>
    {
        protected override ProcessInstanceState BuildFromHistory(IEnumerable<Event> events)
        {
            return new ProcessInstanceState(events);
        }
    }
    #endregion

    #region TaskInstanceAggregate
    public class TaskInstanceState : AggregateState<TaskInstanceState>
    {
        public TaskInstanceState(IEnumerable<Event> events)
        {
            LoadFromHistory(events);
        }
    }

    public class TaskInstance : IAggregateRoot
    {
        TaskInstanceState _state;
        public TaskInstance(TaskInstanceState state)
        {
            _state = state;
        }

        protected void Apply(Event @event)
        {
            _state.Apply(@event, true);
        }

        public void NewMethod(Event @event)
        {
            Apply(@event);
        }
    }

    public class TaskInstanceFactory : EventSourcedConstructor<TaskInstanceState>
    {
        protected override TaskInstanceState BuildFromHistory(IEnumerable<Event> events)
        {
            return new TaskInstanceState(events);
        }
    }
    #endregion TaskInstanceAggregate
}
