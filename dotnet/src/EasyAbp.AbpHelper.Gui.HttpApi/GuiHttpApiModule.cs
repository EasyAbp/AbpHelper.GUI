using EasyAbp.AbpHelper.Gui.Localization;
using Localization.Resources.AbpUi;
using Volo.Abp.Account;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace EasyAbp.AbpHelper.Gui
{
    [DependsOn(
        typeof(GuiServiceModule),
        typeof(AbpAccountHttpApiModule),
        typeof(AbpIdentityHttpApiModule)
    )]
    public class GuiHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureLocalization();
        }

        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<GuiResource>()
                    .AddBaseTypes(
                        typeof(AbpUiResource)
                    );
            });
        }
    }
}
