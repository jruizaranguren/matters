using System;
using System.Diagnostics.CodeAnalysis;
namespace Matters.Core.Domain
{
    [ExcludeFromCodeCoverage]
    public static class SystemTime
    {
        public static Func<DateTime> Now = () => DateTime.Now;
    }
}
