namespace Ludiq
{
    [Plugin(LudiqGraphs.ID)]
    public sealed class LudiqGraphsResources : PluginResources
    {
        private LudiqGraphsResources(LudiqGraphs plugin) : base(plugin)
        {
            icons = new Icons(this);
        }

        public override void Initialize()
        {
            base.Initialize();

            icons.Load();
        }

        public Icons icons { get; private set; }

        public class Icons
        {
            public Icons(LudiqGraphsResources resources)
            {
                this.resources = resources;
            }

            private readonly LudiqGraphsResources resources;

            public EditorTexture window { get; private set; }
            public EditorTexture inspectorWindow { get; private set; }

            public void Load()
            {
                window = resources.LoadIcon("Windows/GraphWindow.png");
                inspectorWindow = resources.LoadIcon("Windows/GraphInspectorWindow.png");

                if (GraphWindow.active != null)
                {
                    GraphWindow.active.titleContent.image = window?[IconSize.Small];
                }

                if (GraphInspectorWindow.instance != null)
                {
                    GraphInspectorWindow.instance.titleContent.image = inspectorWindow?[IconSize.Small];
                }
            }
        }
    }
}