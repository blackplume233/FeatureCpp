namespace Ludiq
{
    [Plugin(LudiqCore.ID)]
    public class LudiqCoreResources : PluginResources
    {
        private LudiqCoreResources(LudiqCore plugin) : base(plugin)
        {
            icons = new Icons(this);
        }

        public override void Initialize()
        {
            base.Initialize();

            icons.Load();

            loader = LoadTexture("Loader/Loader.png", CreateTextureOptions.PixelPerfect);
        }

        public Icons icons { get; private set; }

        public EditorTexture loader { get; private set; }

        public class Icons
        {
            public Icons(LudiqCoreResources resources)
            {
                this.resources = resources;
            }

            private readonly LudiqCoreResources resources;

            public EditorTexture empty { get; private set; }

            public EditorTexture progress { get; private set; }
            public EditorTexture errorState { get; private set; }
            public EditorTexture successState { get; private set; }
            public EditorTexture warningState { get; private set; }

            public EditorTexture informationMessage { get; private set; }
            public EditorTexture questionMessage { get; private set; }
            public EditorTexture warningMessage { get; private set; }
            public EditorTexture successMessage { get; private set; }
            public EditorTexture errorMessage { get; private set; }

            public EditorTexture upgrade { get; private set; }
            public EditorTexture upToDate { get; private set; }
            public EditorTexture downgrade { get; private set; }

            public EditorTexture supportWindow { get; private set; }
            public EditorTexture sidebarAnchorLeft { get; private set; }
            public EditorTexture sidebarAnchorRight { get; private set; }

            public EditorTexture editorPref { get; private set; }
            public EditorTexture projectSetting { get; private set; }

            public EditorTexture @null { get; private set; }

            public void Load()
            {
                empty = EditorTexture.Single(ColorPalette.transparent.GetPixel());

                // Messages
                informationMessage = resources.LoadIcon("Icons/Messages/Information.png");
                questionMessage = resources.LoadIcon("Icons/Messages/Question.png");
                warningMessage = resources.LoadIcon("Icons/Messages/Warning.png");
                successMessage = resources.LoadIcon("Icons/Messages/Success.png");
                errorMessage = resources.LoadIcon("Icons/Messages/Error.png");

                // States
                warningState = resources.LoadIcon("Icons/State/Warning.png");
                successState = resources.LoadIcon("Icons/State/Success.png");
                errorState = resources.LoadIcon("Icons/State/Error.png");
                progress = resources.LoadIcon("Icons/State/Progress.png");

                // Versioning
                upgrade = resources.LoadIcon("Icons/Versioning/Upgrade.png");
                upToDate = resources.LoadIcon("Icons/Versioning/UpToDate.png");
                downgrade = resources.LoadIcon("Icons/Versioning/Downgrade.png");

                // Windows
                supportWindow = resources.LoadIcon("Icons/Windows/SupportWindow.png");
                sidebarAnchorLeft = resources.LoadTexture("Icons/Windows/SidebarAnchorLeft.png", CreateTextureOptions.PixelPerfect);
                sidebarAnchorRight = resources.LoadTexture("Icons/Windows/SidebarAnchorRight.png", CreateTextureOptions.PixelPerfect);

                // Configuration
                editorPref = resources.LoadTexture("Icons/Configuration/EditorPref.png", new TextureResolution[] { 12, 24 }, CreateTextureOptions.PixelPerfect);
                projectSetting = resources.LoadTexture("Icons/Configuration/ProjectSetting.png", new TextureResolution[] { 12, 24 }, CreateTextureOptions.PixelPerfect);

                // Other
                @null = resources.LoadIcon("Icons/Null.png");
            }
        }
    }
}