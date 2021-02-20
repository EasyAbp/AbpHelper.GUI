using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace EasyAbp.AbpHelper.Gui.Blazor
{
    [Dependency(ReplaceServices = true)]
    public class GuiBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "";
        public override string LogoUrl => "/images/app-icon/icon.svg";
        public override string LogoReverseUrl => "/images/app-icon/icon.svg";
    }
}
