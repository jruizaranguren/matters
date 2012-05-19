using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Matters.Core.Domain;

namespace Matters.Core.Tests.Domain
{
    [TestClass]
    public class EventSourcedConstructorTests
    {
        [TestMethod]
        public void Buid_Returns_Event_Collection_From_BuildFromHistory_Implementation()
        {
            var factory = new EventSourcedSampleFactory();

            var expected = factory.Build(GetEvents());

            Assert.IsInstanceOfType(expected, typeof(EventSourcedSample));
            //There is no need for further tests in EventSourcedConstructor Template.
        }

        private IEnumerable<Event> GetEvents()
        {
            yield return new EventSample();
        }

       
    }

    public class EventSourcedSample : AggregateState
    {
        public EventSourcedSample(IEnumerable<Event> events)
        {
            //A real AggregateSTate would call LoadFromHistory(events);
        }
    }

    public class EventSourcedSampleFactory : EventSourcedConstructor<EventSourcedSample>
    {

        protected override EventSourcedSample BuildFromHistory(IEnumerable<Event> events)
        {
            return new EventSourcedSample(events);
        }


    }


    public class EventSample : Event
    {
    }
}
