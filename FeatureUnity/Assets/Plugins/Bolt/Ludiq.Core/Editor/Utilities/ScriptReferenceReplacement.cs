namespace Ludiq
{
    public struct ScriptReferenceReplacement
    {
        public ScriptReference previousReference;

        public ScriptReference newReference;

        public ScriptReferenceReplacement(ScriptReference previousReference, ScriptReference newReference)
        {
            this.previousReference = previousReference;
            this.newReference = newReference;
        }

        public override string ToString()
        {
            return previousReference + " => " + newReference;
        }
    }
}
