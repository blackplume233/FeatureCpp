using UnityEditor;
using UnityEngine;

namespace Ludiq
{
    public sealed class GraphInspectorWindow : GraphSidebarPanelWindow<GraphInspectorPanel>
    {
        protected override GUIContent defaultTitleContent => new GUIContent("Graph Insp.", LudiqGraphs.Icons.inspectorWindow?[IconSize.Small]);

        protected override GraphInspectorPanel CreatePanel(IGraphContext context, GraphInspectorPanel oldPanel)
        {
            return new GraphInspectorPanel(context);
        }

        protected override void OnEnable()
        {
            instance = this;
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            instance = null;
        }

        public static GraphInspectorWindow instance { get; private set; }

        [MenuItem("Window/Graph Inspector", priority = 1)]
        public static void Open()
        {
            if (instance == null)
            {
                GetWindow<GraphInspectorWindow>().Show();
            }
            else
            {
                FocusWindowIfItsOpen<GraphInspectorWindow>();
            }
        }
    }
}