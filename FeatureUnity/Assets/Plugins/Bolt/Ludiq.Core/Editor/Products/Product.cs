using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Ludiq
{
    public abstract class Product : IAboutable
    {
        internal const string VersionString = "1.4.15";
        protected Product()
        {
            id = ProductContainer.GetProductID(GetType());

            _plugins = new List<Plugin>();
            plugins = _plugins.AsReadOnly();
        }

        public virtual void Initialize()
        {
            setupWizard = new SetupWizard(this);
            updateWizard = new UpdateWizard(this);
            aboutWindow = new AboutWindow(this);
            configurationPanel = new ConfigurationPanel(this);
        }

        internal readonly List<Plugin> _plugins;

        public string id { get; }

        public ReadOnlyCollection<Plugin> plugins { get; }

        public virtual bool requiresSetup => plugins.Any(p => !p.configuration.projectSetupCompleted || !p.configuration.editorSetupCompleted);

        public abstract string name { get; }
        public abstract string author { get; }
        public abstract string description { get; }
        public Texture2D logo { get; protected set; }
        public SemanticVersion version => VersionString;

        public virtual string authorLabel => "Author: ";
        public Texture2D authorLogo { get; protected set; }
        public virtual string copyrightHolder => author;
        public virtual int copyrightYear => DateTime.Now.Year;

        public virtual string publisherUrl => null;
        public virtual string websiteUrl => null;
        public virtual string supportUrl => null;
        public virtual string manualUrl => null;
        public virtual string assetStoreUrl => null;
        public string authorUrl => publisherUrl;
        public string url => websiteUrl;

        public abstract string configurationPanelLabel { get; }

        public SetupWizard setupWizard { get; private set; }
        public AboutWindow aboutWindow { get; private set; }
        public ConfigurationPanel configurationPanel { get; private set; }
        public UpdateWizard updateWizard { get; private set; }

        protected virtual string rootFileName => id + ".root";

        private string _packagePath;

        public string packagePath
        {
            get
            {
                if (_packagePath == null)
                {
                    _packagePath = PathUtility.GetRootPath(rootFileName, $"Ludiq/{id}/{rootFileName}", true);
                }

                return _packagePath;
            }
        }
    }
}