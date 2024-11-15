﻿using System.Collections.Generic;
using System.Linq;

namespace Ludiq
{
    public class AcknowledgementsPage : ListPage
    {
        public AcknowledgementsPage(IEnumerable<Plugin> plugins)
        {
            Ensure.That(nameof(plugins)).IsNotNull(plugins);

            title = shortTitle = "Acknowledgements";
            icon = LudiqCore.Resources.LoadIcon("Icons/Windows/AboutWindow/AcknowledgementPage.png");

            foreach (var acknowledgement in plugins.ResolveDependencies().SelectMany(plugin => plugin.resources.acknowledgements))
            {
                pages.Add(new AcknowledgementPage(acknowledgement));
            }
        }
    }
}