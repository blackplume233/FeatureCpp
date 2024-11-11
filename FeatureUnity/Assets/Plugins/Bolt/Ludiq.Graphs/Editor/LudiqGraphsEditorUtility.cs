namespace Ludiq
{
    public static class LudiqGraphsEditorUtility
    {
        public static OverrideStack<IGraphContext> editedContext { get; } = new OverrideStack<IGraphContext>(null);
    }
}