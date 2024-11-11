namespace Ludiq
{
    [Plugin(LudiqCore.ID)]
    internal class Acknowledgement_Iconmonstr : PluginAcknowledgement
    {
        public Acknowledgement_Iconmonstr(Plugin plugin) : base(plugin) { }

        public override string title => "Iconmonstr Icons";
        public override string author => "Alexander Kahlkopf";
        public override string url => "https://iconmonstr.com";
        public override string licenseText => Licenses.Iconmonstr;
    }
}