namespace Ludiq
{
    public interface IPoolable
    {
        void New();
        void Free();
    }
}