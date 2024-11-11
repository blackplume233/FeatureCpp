namespace Ludiq
{
    public interface IGraphNester : IGraphParent
    {
        IGraphNest nest { get; }

        void InstantiateNest();
        void UninstantiateNest();
    }
}