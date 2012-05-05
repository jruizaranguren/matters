using Matters.Core.Domain;
using Matters.Core.Persistence;
using System.Collections.Generic;
using System;

namespace Matters.Core.Tests.Domain
{
    #region ProcessInstanceAggregate
    public class ProcessInstance : AggregateRoot<ProcessInstanceState>
    {
        public ProcessInstance(ProcessInstanceState state)
            : base(state)
        {
        }
    }

    public class ProcessInstanceState : AggregateState
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
    public class TaskInstanceState : AggregateState
    {
        public TaskInstanceState(IEnumerable<Event> events)
        {
            LoadFromHistory(events);
        }
    }

    public class TaskInstance : AggregateRoot<TaskInstanceState>
    {
        public TaskInstance(TaskInstanceState state)
            : base(state)
        { }

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
