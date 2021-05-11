using Volo.Abp.Bundling;

namespace EasyAbp.AbpHelper.Gui.Blazor
{
    public class GuiBundleContributor : IBundleContributor
    {
        public void AddScripts(BundleContext context)
        {
        }

        public void AddStyles(BundleContext context)
        {
            context.Add("main.css", true);
        }
    }
}