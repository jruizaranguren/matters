using System;
using System.Diagnostics.CodeAnalysis;

namespace Matters.Core.Domain
{
    [ExcludeFromCodeCoverage]
    public static class Identities
    {
        public static readonly Func<Guid> NewGuid = () => Guid.NewGuid();
    }
}
