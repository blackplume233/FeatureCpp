namespace Ludiq
{
    public interface IConnection<out TSource, out TDestination>
    {
        TSource source { get; }
        TDestination destination { get; }
    }
}