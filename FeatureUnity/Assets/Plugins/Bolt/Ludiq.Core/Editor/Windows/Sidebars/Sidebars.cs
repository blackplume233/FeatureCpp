using System.Collections.Generic;

namespace Ludiq
{
    public sealed class Sidebars
    {
        public Sidebars()
        {
            left = new Sidebar(this, SidebarAnchor.Left);
            right = new Sidebar(this, SidebarAnchor.Right);
        }

        [Serialize]
        public Sidebar left { get; private set; }

        [Serialize]
        public Sidebar right { get; private set; }

        [Serialize]
        public List<SidebarPanel> panels { get; private set; } = new List<SidebarPanel>();

        [DoNotSerialize]
        public Sidebar this[SidebarAnchor anchor]
        {
            get
            {
                switch (anchor)
                {
                    case SidebarAnchor.Left: return left;
                    case SidebarAnchor.Right: return right;
                    default: throw new UnexpectedEnumValueException<SidebarAnchor>(anchor);
                }
            }
        }

        public void Feed(IEnumerable<ISidebarPanelContent> panelContents)
        {
            Ensure.That(nameof(panelContents)).IsNotNull(panelContents);

            foreach (var panel in panels)
            {
                panel.Disable();
            }

            foreach (var panelContent in panelContents)
            {
                var associated = false;

                foreach (var panel in panels)
                {
                    if (panel.TryAssociate(panelContent))
                    {
                        associated = true;
                        break;
                    }
                }

                if (!associated)
                {
                    panels.Add(new SidebarPanel(panelContent));
                }

                foreach (var panel in panels)
                {
                    panel.sidebars = this;
                }
            }
        }
    }
}
