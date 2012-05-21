using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Matters.Core.Domain;
using Matters.Core.Persistence;
using EventStore;
using EventStore.Dispatcher;

namespace Matters.Core.Tests.Persistence
{
    [TestClass]
    public class JOliverEventSourceTests
    {
        JOliverEventSource source;

        [TestInitialize]
        public void Init()
        {
            var state = new JOliverAggregateState(Enumerable.Empty<Event>());
            var factory = new StoreEventsFactory("", new NullDispatcher());
            source = new JOliverEventSource(factory.GetInMemoryStoreEvents());
        }

        [TestMethod]
        public void GetEvents_Retrieves_Events_For_Id()
        {
        }

        [TestMethod]
        public void SaveEvents_Stores_Events()
        {
        }

    }

    public class JOliverAggregateState : AggregateState<JOliverAggregateState>
    {
        private int _myProperty = 0;

        public JOliverAggregateState(IEnumerable<Event> events)
        {
            LoadFromHistory(events);
        }

        public void Apply(StartAggregateStateEvent @event)
        {
            Id = Guid.Parse("{ABEDEA43-08EB-4428-BFF6-B3C98628C8D0}");
        }

        public int GetMyProperty()
        {
            return _myProperty;
        }

        public void Apply(JOliverEvent2 @event)
        {
            _myProperty = 10;
        }
    }

    public class StartAggregateStateEvent : Event { }

    public class JOliverEvent2 : Event { }

    public class JOliverAggregateFactory : EventSourcedConstructor<JOliverAggregateState>
    {
        protected override JOliverAggregateState BuildFromHistory(IEnumerable<Event> events)
        {
            return new JOliverAggregateState(events);
        }
    }

}
