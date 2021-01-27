using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace EasyAbp.AbpHelper.Gui
{
    [Dependency(ReplaceServices = true)]
    public class GuiBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Gui";
    }
}
