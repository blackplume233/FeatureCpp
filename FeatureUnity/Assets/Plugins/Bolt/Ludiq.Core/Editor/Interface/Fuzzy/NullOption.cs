namespace Ludiq
{
    public sealed class NullOption : FuzzyOption<object>
    {
        public NullOption()
        {
            label = "(None)";
            value = null;
        }
    }
}