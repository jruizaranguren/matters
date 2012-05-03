using System;
using System.Collections.Generic;


namespace Matters.Core.Domain
{
    public interface IRepository<TAggregateState> where TAggregateState : IAggregateState
    {
        int NewElementVersion { get; }
        void Save(TAggregateState aggregate, int expectedVersion);
        TAggregateState GetById(Guid id);
    }

}
