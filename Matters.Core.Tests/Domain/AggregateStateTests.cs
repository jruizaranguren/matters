using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Matters.Core.Domain;

namespace Matters.Core.Tests.Domain
{
    [TestClass]
    public class AggregateStateTests
    {
        [TestMethod]
        public void AggregateState_Constructor_BuildsFromHistory()
        {
            var state = new AggregateStateSample(GetInitialEvents());
            int expectedVersion = 1;

            Assert.AreEqual(expectedVersion,state.Version);
            Assert.AreEqual(state.GetUncommittedEvents().Count(), 0);
        }

        [TestMethod]
        public void Apply_NewEvent_UpdatesVersionAndUncommittedEvents()
        {
            var state = new AggregateStateSample(GetInitialEvents());
            int expectedVersion = 2;

            state.Apply(new Event2(), true);

            Assert.AreEqual(expectedVersion, state.Version);
            Assert.AreEqual(state.GetUncommittedEvents().Count(), 1);
            Assert.IsInstanceOfType(state.GetUncommittedEvents().First(), typeof(Event2));
        }

        [TestMethod]
        public void Apply_EventFromHistory_DoesntUpdateVersionAndUncommittedEvents()
        {
            var state = new AggregateStateSample(GetInitialEvents());
            int expectedVersion = 1;

            state.Apply(new Event2(), false);

            Assert.AreEqual(expectedVersion, state.Version);
            Assert.AreEqual(state.GetUncommittedEvents().Count(), 0);
        }

        [TestMethod]
        public void MarkChangesAsCommited_ClearsChanges()
        {
            var state = new AggregateStateSample(GetInitialEvents());
            int expectedVersion = 2;

            state.Apply(new Event2(), true);
            state.MarkChangesAsCommited();

            Assert.AreEqual(expectedVersion, state.Version);
            Assert.AreEqual(state.GetUncommittedEvents().Count(), 0);
        }

        private IEnumerable<Event> GetInitialEvents()
        {
            yield return new Event1();
        }

        private IEnumerable<Event> GetExpectedUncommitedEvents()
        {
            yield return new Event2();
        }
    }

    public class AggregateStateSample : AggregateState
    {
        public AggregateStateSample(IEnumerable<Event> events)
        {
            LoadFromHistory(events);
        }

        public void Apply(Event1 @event)
        { 
        }

        public void Apply(Event2 @event)
        { }
    }

    public class Event1 : Event 
    {
        public Event1()
        {
            SetActualVersion(1);
        }
    }

    public class Event2 : Event { }

}
