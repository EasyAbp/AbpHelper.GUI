using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace EasyAbp.AbpHelper.Gui
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpTestBaseModule),
        typeof(AbpAuthorizationModule)
    )]
    public class GuiTestBaseModule : AbpModule
    {
    }
}
