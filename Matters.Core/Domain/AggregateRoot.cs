namespace Matters.Core.Domain
{
    public class AggregateRoot<T> : IAggregateRoot where T : AggregateState
    {
        protected T _state;

        public AggregateRoot(T state)
        {
            _state = state;
        }

        protected void Apply(Event @event)
        {
            _state.Apply(@event, true);
        }
    }
}
