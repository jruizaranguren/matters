using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Matters.Core.Domain;

namespace Matters.Core.Tests.Domain
{
    [TestClass]
    public class FindApplyTests
    {
        [TestMethod]
        public void InvokeEvent_Calls_Apply_Method_In_Object()
        {
            var sampleObject = new SampleClass();

            FindApply.InvokeEvent(sampleObject, new EventToApply());

            Assert.IsTrue(sampleObject.Applied);
        }

        //TODO, test that throws exception
        //TODO, test that demonstrates cache use
    }

    public class SampleClass
    {
        public bool Applied { get; private set; }

        public SampleClass()
        {
            Applied = false;
        }

        public void Apply(EventToApply @event)
        {
            Applied = true;
        }
    }

    public class EventToApply : Event { }
}
