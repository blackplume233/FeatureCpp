namespace Ludiq
{
    public interface IAnalyser
    {
        IAnalysis analysis { get; }

        bool isDirty { get; set; }

        void Validate();
    }
}
