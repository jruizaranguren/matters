using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Matters.Core.Persistence;
using Matters.Core.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matters.Core.Tests.Persistence
{
    [TestClass]
    public class FakeEventSourceTests
    {
        [TestMethod]
        public void FakeStore_Builds_From_Events()
        {
            int expectedUncommited = 0;
            int expectedCommited = 2;
            
            var sut = new FakeEventSource(GenerateEvents());
            var commitedEvents = sut.GetEvents(Guid.NewGuid());

            Assert.AreEqual(expectedUncommited, sut.GetUncommitedEvents().Count());
            Assert.AreEqual(expectedCommited, commitedEvents.Count());
            Assert.IsInstanceOfType(commitedEvents.First(), typeof(FakeEvent1));
            Assert.IsInstanceOfType(commitedEvents.Last(), typeof(FakeEvent2));
        }

        [TestMethod]
        public void SaveEvents_Save_Events_As_UnCommited()
        {
            int expectedCommited = 0;
            int expectedUncommited = 2;
           
            var sut = new FakeEventSource(Enumerable.Empty<Event>());
            sut.SaveEvents(Guid.NewGuid(), GenerateEvents(), 0);

            var uncommitedEvents = sut.GetUncommitedEvents();

            Assert.AreEqual(expectedUncommited, uncommitedEvents.Count());
            Assert.AreEqual(expectedCommited, sut.GetEvents(Guid.NewGuid()).Count());
            Assert.IsInstanceOfType(uncommitedEvents.First(), typeof(FakeEvent1));
            Assert.IsInstanceOfType(uncommitedEvents.Last(), typeof(FakeEvent2));
        }

        public IEnumerable<Event> GenerateEvents()
        {
            yield return new FakeEvent1();
            yield return new FakeEvent2();
        }
    }

    public class FakeEvent1 : Event { }
    public class FakeEvent2 : Event { }
}
