namespace Ludiq
{
    public interface IPluginModule : IPluginLinked
    {
        void Initialize();
        void LateInitialize();
    }
}