using EasyAbp.AbpHelper.Gui.Localization;
using Volo.Abp.Account;
using Volo.Abp.Cli;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace EasyAbp.AbpHelper.Gui
{
    [DependsOn(
        typeof(AbpCliCoreModule),
        typeof(GuiApplicationContractsModule)
    )]
    public class GuiServiceModule : AbpModule
    {
    }
}
