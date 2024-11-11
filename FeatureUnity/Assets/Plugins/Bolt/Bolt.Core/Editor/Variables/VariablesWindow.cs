using UnityEditor;
using UnityEngine;
using Ludiq;

namespace Bolt
{
    public sealed class VariablesWindow : GraphSidebarPanelWindow<VariablesPanel>
    {
        protected override GUIContent defaultTitleContent => new GUIContent("Variables", BoltCore.Icons.variablesWindow?[IconSize.Small]);

        protected override VariablesPanel CreatePanel(IGraphContext context, VariablesPanel oldPanel)
        {
            var panel = new VariablesPanel(context);
            panel.currentSubTabIdentifier = oldPanel?.currentSubTabIdentifier;
            return panel;
        }

        private void _OnSelectionChange()
        {
            UpdatePanel();
        }

        private void _OnProjectChange()
        {
            UpdatePanel();
        }

        private void _OnHierarchyChange()
        {
            // For some odd reason, OnHierarchyChange seems to trigger 
            // when changes that affect prefab instances or definitions,
            // causing a panel update and focus loss when typing. 
            // Disable for now.
            // UpdatePanel();
        }

        private void _OnModeChange()
        {
            UpdatePanel();
        }

        protected override void OnEnable()
        {
            instance = this;
            base.OnEnable();

            // Manual handlers have to be used over magic methods because
            // magic methods don't get triggered when the window is out of focus
            EditorApplicationUtility.onSelectionChange += _OnSelectionChange;
            EditorApplicationUtility.onProjectChange += _OnProjectChange;
            EditorApplicationUtility.onHierarchyChange += _OnHierarchyChange;
            EditorApplicationUtility.onModeChange += _OnModeChange;
            PrefabUtility.prefabInstanceUpdated += _OnPrefabInstanceUpdate;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            instance = null;

            EditorApplicationUtility.onSelectionChange -= _OnSelectionChange;
            EditorApplicationUtility.onProjectChange -= _OnProjectChange;
            EditorApplicationUtility.onHierarchyChange -= _OnHierarchyChange;
            EditorApplicationUtility.onModeChange -= _OnModeChange;
            PrefabUtility.prefabInstanceUpdated -= _OnPrefabInstanceUpdate;
        }

        private void _OnPrefabInstanceUpdate(GameObject gameObject)
        {
            UpdatePanel();
        }

        public static VariablesWindow instance { get; private set; }

        [MenuItem("Window/Variables", priority = 3)]
        public static void Open()
        {
            if (instance == null)
            {
                GetWindow<VariablesWindow>().Show();
            }
            else
            {
                FocusWindowIfItsOpen<VariablesWindow>();
            }
        }
    }
}