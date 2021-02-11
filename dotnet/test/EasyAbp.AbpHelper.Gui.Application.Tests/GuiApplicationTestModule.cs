using Volo.Abp.Modularity;

namespace EasyAbp.AbpHelper.Gui
{
    [DependsOn(
        typeof(GuiApplicationModule),
        typeof(GuiTestBaseModule)
        )]
    public class GuiApplicationTestModule : AbpModule
    {

    }
}
