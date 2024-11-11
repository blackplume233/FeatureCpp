using Ludiq;

namespace Bolt
{
    [Plugin(BoltCore.ID)]
    public sealed class BoltCoreResources : PluginResources
    {
        private BoltCoreResources(BoltCore plugin) : base(plugin)
        {
            icons = new Icons(this);
        }

        public Icons icons { get; private set; }

        public override void LateInitialize()
        {
            base.LateInitialize();

            icons.Load();
        }

        public class Icons
        {
            public EditorTexture variablesWindow { get; private set; }

            public EditorTexture variable { get; private set; }
            public EditorTexture flowVariable { get; private set; }
            public EditorTexture graphVariable { get; private set; }
            public EditorTexture objectVariable { get; private set; }
            public EditorTexture sceneVariable { get; private set; }
            public EditorTexture applicationVariable { get; private set; }
            public EditorTexture savedVariable { get; private set; }

            public Icons(BoltCoreResources resources)
            {
                this.resources = resources;
            }

            private readonly BoltCoreResources resources;

            public void Load()
            {
                variablesWindow = resources.LoadIcon("Icons/Windows/VariablesWindow.png");

                variable = resources.LoadIcon("Icons/Variables/Variable.png");
                flowVariable = resources.LoadIcon("Icons/Variables/FlowVariable.png");
                graphVariable = resources.LoadIcon("Icons/Variables/GraphVariable.png");
                objectVariable = resources.LoadIcon("Icons/Variables/ObjectVariable.png");
                sceneVariable = resources.LoadIcon("Icons/Variables/SceneVariable.png");
                applicationVariable = resources.LoadIcon("Icons/Variables/ApplicationVariable.png");
                savedVariable = resources.LoadIcon("Icons/Variables/SavedVariable.png");

                if (VariablesWindow.instance != null)
                {
                    VariablesWindow.instance.titleContent.image = variablesWindow?[IconSize.Small];
                }
            }

            public EditorTexture VariableKind(VariableKind kind)
            {
                switch (kind)
                {
                    case Bolt.VariableKind.Flow: return flowVariable;
                    case Bolt.VariableKind.Graph: return graphVariable;
                    case Bolt.VariableKind.Object: return objectVariable;
                    case Bolt.VariableKind.Scene: return sceneVariable;
                    case Bolt.VariableKind.Application: return applicationVariable;
                    case Bolt.VariableKind.Saved: return savedVariable;
                    default: throw new UnexpectedEnumValueException<VariableKind>(kind);
                }
            }
        }
    }
}