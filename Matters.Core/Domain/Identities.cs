using System;

namespace Matters.Core.Domain
{
    public static class Identities
    {
        public static readonly Func<Guid> NewGuid = () => Guid.NewGuid();
    }
}
