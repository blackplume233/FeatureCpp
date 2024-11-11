namespace Ludiq
{
    public interface IDescriptor
    {
        object target { get; }

        IDescription description { get; }

        bool isDirty { get; set; }

        void Validate();
    }
}