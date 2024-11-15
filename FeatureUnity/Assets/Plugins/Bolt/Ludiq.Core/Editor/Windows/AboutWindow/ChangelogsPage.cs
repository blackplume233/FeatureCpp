﻿using System.Collections.Generic;
using System.Linq;

namespace Ludiq
{
    public class ChangelogsPage : ListPage
    {
        public ChangelogsPage(IEnumerable<Plugin> plugins)
        {
            Ensure.That(nameof(plugins)).IsNotNull(plugins);

            title = "Changelogs";
            shortTitle = "Changelogs";
            icon = LudiqCore.Resources.LoadIcon("Icons/Windows/AboutWindow/ChangelogPage.png");

            foreach (var changelog in plugins.ResolveDependencies().SelectMany(plugin => plugin.resources.changelogs).OrderByDescending(changelog => changelog.date))
            {
                pages.Add(new ChangelogPage(changelog, plugins.Count() > 1));
            }
        }
    }
}