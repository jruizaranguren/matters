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

        [TestMethod]
        public void InvokeEvent_Apply_Method_Does_Not_Exist_Nothing_Happens()
        {
            var sampleObject = new SampleClass();

            FindApply.InvokeEvent(sampleObject, new UnHandledEvent());

            Assert.IsFalse(sampleObject.Applied);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),"Error")]
        public void InvokeEvent_When_Exception_Is_Thrown_Concrete_Exception_Is_Catched()
        {
            var sampleObject = new SampleClass();

            FindApply.InvokeEvent(sampleObject, new EventThatOriginateException());
        }
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

        public void Apply(EventThatOriginateException @event)
        {
            throw new ArgumentException("Error");
        }
    }

    public class EventToApply : Event { }

    public class EventThatOriginateException : Event { }

    public class UnHandledEvent : Event { }
}
