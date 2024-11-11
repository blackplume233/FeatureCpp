using UnityEngine;

namespace Ludiq
{
    public class SetupWizard : Wizard
    {
        public SetupWizard(Product product)
        {
            Ensure.That(nameof(product)).IsNotNull(product);

            this.product = product;

            pages.Add(new WelcomePage(product));

            foreach (var plugin in product.plugins.ResolveDependencies())
            {
                var pluginPages = plugin.SetupWizardPages();

                if (pluginPages != null)
                {
                    pages.AddRange(pluginPages);
                }
            }

            pages.Add(new SetupCompletePage(product));
        }

        private readonly Product product;

        protected override void ConfigureWindow()
        {
            window.titleContent = new GUIContent($"{product.name} Setup Wizard");
            window.minSize = window.maxSize = new Vector2(530, 400);
        }
    }
}