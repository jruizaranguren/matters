using EventStore.Dispatcher;
using EventStore;
using System.Diagnostics.CodeAnalysis;

namespace Matters.Core.Persistence
{
    [ExcludeFromCodeCoverage]
    public sealed class StoreEventsFactory
    {
        private string _connectionString;
        private IDispatchCommits _dispatcher;

        public StoreEventsFactory(string connectionString, IDispatchCommits dispatcher)
        {
            _dispatcher = dispatcher;
            _connectionString = connectionString;
        }

        public IStoreEvents GetSqlServerStoreEvents()
        {
            IStoreEvents storeEvents = Wireup.Init()
                  .LogToOutputWindow()
                  .UsingSqlPersistence(_connectionString)
                  .EnlistInAmbientTransaction() // TODO review
                  .InitializeStorageEngine()
                  .UsingBinarySerialization()
                  .UsingAsynchronousDispatchScheduler()
                  .DispatchTo(_dispatcher)
                  .Build();
            return storeEvents;
        }

        public IStoreEvents GetInMemoryStoreEvents()
        {
            IStoreEvents storeEvents = Wireup.Init()
                .LogToOutputWindow()
                .UsingInMemoryPersistence()
                .EnlistInAmbientTransaction()
                .InitializeStorageEngine()
                .UsingBinarySerialization()
                .UsingAsynchronousDispatchScheduler()
                .DispatchTo(_dispatcher)
                .Build();
            return storeEvents;
        }

    }
}