using System;

namespace Ludiq
{
    [Plugin(LudiqCore.ID)]
    internal abstract class LudiqCoreMigration : PluginMigration
    {
        protected LudiqCoreMigration(Plugin plugin) : base(plugin) { }

        protected void AddDefaultTypeOption(Type typeOption)
        {
            if (!LudiqCore.Configuration.typeOptions.Contains(typeOption))
            {
                LudiqCore.Configuration.typeOptions.Add(typeOption);
                LudiqCore.Configuration.Save();
            }
        }

        protected void AddDefaultAssemblyOption(string assemblyOption)
        {
            if (!LudiqCore.Configuration.assemblyOptions.Contains(assemblyOption))
            {
                LudiqCore.Configuration.assemblyOptions.Add(assemblyOption);
                LudiqCore.Configuration.Save();
            }
        }
    }
}
