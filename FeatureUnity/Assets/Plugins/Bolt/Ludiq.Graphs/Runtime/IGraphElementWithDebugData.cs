namespace Ludiq
{
    public interface IGraphElementWithDebugData : IGraphElement
    {
        IGraphElementDebugData CreateDebugData();
    }
}
