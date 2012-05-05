using System;
namespace Matters.Core.Persistence
{
    public class AggregateNotFoundException : Exception
    {
    }

    public class ConcurrencyException : Exception
    {
    }
}
