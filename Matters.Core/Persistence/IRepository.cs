using System;
using System.Collections.Generic;
using Matters.Core.Domain;

namespace Matters.Core.Persistence
{
    public interface IRepository<TEventSourced> where TEventSourced : IEventSourced
    {
        void Save(TEventSourced eventSourced, int expectedVersion);
        TEventSourced GetById(Guid id);
    }
}
