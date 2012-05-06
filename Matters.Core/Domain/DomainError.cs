using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Matters.Core.Domain
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class DomainError : Exception
    {
        public DomainError() { }
        public DomainError(string message) : base(message) { }
        public DomainError(string format, params object[] args) : base(string.Format(format, args)) { }

        /// <summary>
        /// Creates domain error exception with a string name, that is easily identifiable in the tests
        /// </summary>
        /// <param name="name">The name to be used to identify this exception in tests.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public static DomainError Named(string name, string format, params object[] args)
        {
            var message = string.Concat("[",name,"] ",string.Format(format, args));
            return new DomainError(message)
            {
                Name = name
            };
        }

        public string Name { get; private set; }

        public DomainError(string message, Exception inner) : base(message, inner) { }

        protected DomainError(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context) { }
    }
}
