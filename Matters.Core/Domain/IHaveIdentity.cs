namespace Matters.Core.Domain
{
    public interface IHaveIdentity<T>
    {
        T Id { get; }
    }
}
