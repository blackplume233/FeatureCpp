﻿using System.Collections.Generic;

namespace Ludiq
{
    public class AboutPluginsPage : ListPage
    {
        public AboutPluginsPage(IEnumerable<Plugin> plugins)
        {
            Ensure.That(nameof(plugins)).IsNotNull(plugins);

            title = "About Plugins";
            shortTitle = "Plugins";
            icon = LudiqCore.Resources.LoadIcon("Icons/Windows/AboutWindow/AboutPluginsPage.png");

            foreach (var plugin in plugins)
            {
                pages.Add(new AboutablePage(plugin.manifest));
            }
        }
    }
}