using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Matters.Core.Domain;

namespace Matters.Core.Tests.Domain
{
    [TestClass]
    public class EnumerationBaseTests
    {
        [TestMethod]
        public void RawEnumerationClass_IsCorrectlyInitialized()
        {
            var expectedNumberItems = 3;
            var expectedCode = Guid.Parse("{5CA84018-AB60-4D7C-808B-26EB400D6F25}");
               
            var actualNumberItems = RawEnumerationClass.GetValues().Count();

            Assert.AreEqual(expectedNumberItems, actualNumberItems);
            Assert.AreEqual(expectedCode.GetHashCode(),RawEnumerationClass.FirstValue.GetHashCode());
            Assert.AreEqual(RawEnumerationClass.FirstValue, RawEnumerationClass.Default);
        }

        [TestMethod]
        public void CustomTypeEnumerationClass_IsCorrectlyInitialized()
        {
            var expectedNumberItems = 3;
            var expectedVat = 0.07M;
            var actualNumberItems = CustomTypeEnumerationClass.GetValues().Count();
            Assert.AreEqual(expectedNumberItems, actualNumberItems);
            Assert.AreEqual(expectedVat, CustomTypeEnumerationClass.FirstValue.Value.Vat);
        }

        [TestMethod]
        public void Equals_WithEnumerationBaseArgument_Works()
        {
            var expected = RawEnumerationClass.FirstValue;
            Assert.IsTrue(expected.Equals(RawEnumerationClass.FirstValue));
            Assert.IsFalse(expected.Equals(RawEnumerationClass.SecondValue));
            Assert.IsFalse(RawEnumerationClass.SecondValue.Equals(null));
        }

        [TestMethod]
        public void Equals_WithObjectArgument_Works()
        { 
            var expected = RawEnumerationClass.FirstValue;
            var notExpected = RawEnumerationClass.SecondValue;
            
            Assert.IsFalse(RawEnumerationClass.SecondValue.Equals(new object()));
            Assert.IsFalse(RawEnumerationClass.SecondValue.Equals((object)null));
            Assert.IsTrue(RawEnumerationClass.FirstValue.Equals((object)expected));
            Assert.IsFalse(RawEnumerationClass.FirstValue.Equals((object)notExpected));
         }

        [TestMethod]
        public void EqualityOperators_Works()
        {
            var consumer = RawEnumerationClass.FirstValue;
            
            Assert.IsTrue(consumer == RawEnumerationClass.FirstValue);
            Assert.IsFalse(consumer == RawEnumerationClass.SecondValue);
            Assert.IsFalse(consumer == (RawEnumerationClass)null);
            Assert.IsFalse((RawEnumerationClass)null == consumer);
            Assert.IsTrue(consumer != RawEnumerationClass.SecondValue);
            Assert.IsTrue((RawEnumerationClass)null == (RawEnumerationClass)null);
        }
    }

    /// <summary>
    /// Example of a class with static values
    /// </summary>
    public sealed class RawEnumerationClass : EnumerationBase<RawEnumerationClass, Guid, string>
    {
        static RawEnumerationClass()
        {
            FirstValue = Register(new Guid("5CA84018-AB60-4D7C-808B-26EB400D6F25"), "First Value");
            SecondValue = Register(new Guid("6DFED3E6-AD72-4605-975C-72A5A2880F6C"), "Second Value");
            ThirdValue = Register(new Guid("CFD1C6FB-5F75-4B35-A5AC-26F0D019A9C7"), "Third Value");
            DefaultTo(FirstValue);
        }

        public static readonly RawEnumerationClass FirstValue;
        public static readonly RawEnumerationClass SecondValue;
        public static readonly RawEnumerationClass ThirdValue;

    }

    /// <summary>
    /// Example with a Custom class as TValue
    /// </summary>
    public sealed class CustomTypeEnumerationClass : EnumerationBase<CustomTypeEnumerationClass, Guid, CustomType>
    {
        static CustomTypeEnumerationClass()
        {
            FirstValue = Register(new Guid("5CA84018-AB60-4D7C-808B-26EB400D6F25"), new CustomType("First Value", 0.07M));
            SecondValue = Register(new Guid("6DFED3E6-AD72-4605-975C-72A5A2880F6C"), new CustomType("Second Value", 0.19M));
            ThirdValue = Register(new Guid("CFD1C6FB-5F75-4B35-A5AC-26F0D019A9C7"), new CustomType("Third Value", 0));
        }

        public static readonly CustomTypeEnumerationClass FirstValue;
        public static readonly CustomTypeEnumerationClass SecondValue;
        public static readonly CustomTypeEnumerationClass ThirdValue;

        public bool IsFirstValue()
        {
            return ReferenceEquals(this, FirstValue);
        }
    }

    public sealed class CustomType
    {
        public string Description { get; private set; }
        public decimal Vat { get; private set; }

        public CustomType(string description, decimal vat)
        {
            Description = description;
            Vat = vat;
        }
    }

    public sealed class RawEnumerationConsumer
    {
        private RawEnumerationClass _rawInstance;

        public RawEnumerationClass RawInstance
        {
            get { return _rawInstance; }
        }

        public RawEnumerationConsumer()
        {
            _rawInstance = RawEnumerationClass.FirstValue;
        }

    }

}