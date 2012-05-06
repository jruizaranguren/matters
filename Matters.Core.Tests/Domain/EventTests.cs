using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Matters.Core.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matters.Core.Tests.Domain
{
    [TestClass]
    public class EventTests
    {
        [TestMethod]
        public void SetActualVersion_Updates_VersionProperty()
        {
            SampleEvent @event = new SampleEvent();
            int expected = 3;

            @event.SetActualVersion(3);

            Assert.AreEqual(expected, @event.Version);
        }

        [TestMethod]
        public void OccursNow_Sets_Timestamp_ToNow()
        { 
            SystemTime.Now = () => new DateTime(2012, 5, 5);
            var expected = new DateTime(2012, 5, 5);
            var @event = new SampleEvent();

            @event.OccursNow();
   
            Assert.AreEqual(expected,@event.Timestamp);
        }
    }

    public class SampleEvent : Event
    { 
    }
}
