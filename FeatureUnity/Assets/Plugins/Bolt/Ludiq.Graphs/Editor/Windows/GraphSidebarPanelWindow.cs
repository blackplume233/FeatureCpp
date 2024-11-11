using UnityEngine;

namespace Ludiq
{
    public abstract class GraphSidebarPanelWindow<TPanelContent> : SidebarPanelWindow<TPanelContent>
        where TPanelContent : class, ISidebarPanelContent
    {
        protected abstract TPanelContent CreatePanel(IGraphContext context, TPanelContent oldContent);

        protected override void OnEnable()
        {
            GraphWindow.activeContextChanged += OnContextChange;

            PluginContainer.delayCall += () => OnContextChange(GraphWindow.activeContext);
        }

        protected override void OnDisable()
        {
            GraphWindow.activeContextChanged -= OnContextChange;
        }

        protected IGraphContext context;

        protected virtual bool requiresContext => false;

        protected void UpdatePanel()
        {
            if (context == null && requiresContext)
            {
                panel = null;
            }
            else
            {
                panel = CreatePanel(context, panel);
                minSize = panel.minSize;
            }

            Repaint();
        }

        protected void OnContextChange(IGraphContext context)
        {
            if (context == null || !context.reference.isValid)
            {
                this.context = null;
            }
            else
            {
                this.context = context;
            }

            UpdatePanel();
        }

        protected override void BeforeGUI()
        {
            base.BeforeGUI();
            GraphWindow.active?.Validate();
        }
    }
}
