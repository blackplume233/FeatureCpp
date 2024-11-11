namespace Ludiq
{
    public interface IGraphElementWithData : IGraphElement
    {
        IGraphElementData CreateData();
    }
}
