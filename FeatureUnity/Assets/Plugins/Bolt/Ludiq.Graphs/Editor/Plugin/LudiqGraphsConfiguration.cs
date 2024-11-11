using UnityEngine;

namespace Ludiq
{
    [Plugin(LudiqGraphs.ID)]
    public sealed class LudiqGraphsConfiguration : PluginConfiguration
    {
        private LudiqGraphsConfiguration(LudiqGraphs plugin) : base(plugin) { }

        #region Editor Prefs

        /// <summary>
        /// Whether the graph window should show the background grid.
        /// </summary>
        [EditorPref]
        public bool showGrid { get; set; } = true;

        /// <summary>
        /// Whether graph elements should snap to grid.
        /// </summary>
        [EditorPref]
        public bool snapToGrid { get; set; } = false;

        /// <summary>
        /// The window size at which the graph window should start displaying a sidebar.
        /// </summary>
        [EditorPref]
        public Vector2 graphSidebarThreshold { get; set; } = new Vector2(1000, 700);

        /// <summary>
        /// The speed at which the mouse scroll pans the graph.
        /// </summary>
        [EditorPref]
        [InspectorRange(1, 20)]
        public float panSpeed { get; set; } = 5;

        /// <summary>
        /// The speed at which dragged elements pan the graph when at the edge.
        /// </summary>
        [EditorPref]
        [InspectorRange(0, 10)]
        public float dragPanSpeed { get; set; } = 5;

        /// <summary>
        /// The speed at which the mouse wheel zooms the graph.
        /// </summary>
        [EditorPref]
        [InspectorRange(0.01f, 0.1f)]
        public float zoomSpeed { get; set; } = 0.025f;

        /// <summary>
        /// The duration for graph overview. Set to zero to disable smoothing.
        /// </summary>
        [EditorPref]
        [InspectorRange(0, 1)]
        public float overviewSmoothing { get; set; } = 0.25f;

        /// <summary>
        /// Whether children of graph elements should be dragged alongside their parent.
        /// </summary>
        [EditorPref]
        public bool carryChildren { get; set; } = false;

        /// <summary>
        /// Whether the playmode tint should be removed in the graph window.
        /// </summary>
        [EditorPref]
        public bool disablePlaymodeTint { get; set; } = true;

        /// <summary>
        /// Whether additional helpers should be shown in graphs for debugging.
        /// </summary>
        [EditorPref(visibleCondition = nameof(developerMode))]
        public bool debug { get; set; } = false;

        /// <summary>
        /// The control scheme to use for pan and zoom.
        /// Unity: pan with [MMB], zoom with [Ctrl + Scroll Wheel].
        /// Unreal: pan with [MMB] or [Alt + LMB], zoom with [Scroll Wheel].
        /// </summary>
        [EditorPref]
        public CanvasControlScheme controlScheme { get; set; } = CanvasControlScheme.Unreal;

        /// <summary>
        /// Whether the graph window and inspector should be cleared when
        /// the selection does not provide a graph. When disabled,
        /// the last graph will stay selected.
        /// </summary>
        [EditorPref]
        public bool clearGraphSelection { get; set; } = false;

        #endregion
    }
}