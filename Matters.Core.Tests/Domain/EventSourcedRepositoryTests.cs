using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Matters.Core.Domain;
using Matters.Core.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace Matters.Core.Tests.Domain
{
    [TestClass]
    public class EventSourcedRepositoryTests
    {
        [TestMethod]
        public void GetById_Gets_AggregateState()
        {
            int expectedEvents = 0;
            SystemTime.Now = () => new DateTime(2012, 05, 05);

            var sut = new EventSourcedRepository<RepoTestAggregateState>(
                new RepoTestFactory(),
                new FakeEventSource(GenerateEvents()));

            var aggregateState = sut.GetById(Guid.NewGuid());

            Assert.IsInstanceOfType(aggregateState, typeof(RepoTestAggregateState));
            Assert.AreEqual(expectedEvents, aggregateState.GetUncommittedEvents().Count());
            Assert.AreEqual(aggregateState.Id, Guid.Parse("{192BD994-24BB-48C8-A338-2D0F191C2E52}"));
        }

        public IEnumerable<Event> GenerateEvents()
        {
            yield return new RepoEvent();
        }
    }

    public class RepoTestAggregateState : AggregateState<RepoTestAggregateState>
    {
        public RepoTestAggregateState(IEnumerable<Event> events)
        {
            LoadFromHistory(events);
        }

        public void Apply(RepoEvent @event)
        {
            Id = Guid.Parse("{192BD994-24BB-48C8-A338-2D0F191C2E52}");
        }
    }

    public class RepoTestFactory : EventSourcedConstructor<RepoTestAggregateState>
    {
        protected override RepoTestAggregateState BuildFromHistory(IEnumerable<Event> events)
        {
            return new RepoTestAggregateState(events);
        }
    }

    public class RepoEvent : Event 
    {
        public RepoEvent()
        {
            OccursNow();
        }
    }
}
