namespace Matters.Core.Domain
{
    public interface IAggregateState
    {
        void Apply(IEvent @event);
    }
}
