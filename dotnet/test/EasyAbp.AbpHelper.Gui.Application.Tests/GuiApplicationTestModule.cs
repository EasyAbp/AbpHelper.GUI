using System.IO;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace EasyAbp.AbpHelper.Gui
{
    [DependsOn(
        typeof(GuiApplicationModule),
        typeof(GuiTestBaseModule)
        )]
    public class GuiApplicationTestModule : AbpModule
    {
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            if (!Directory.Exists(GuiTestConsts.Folder))
            {
                Directory.CreateDirectory(GuiTestConsts.Folder);
            }
        }

        public override void OnApplicationShutdown(ApplicationShutdownContext context)
        {
            if (Directory.Exists(GuiTestConsts.Folder))
            {
                Directory.Delete(GuiTestConsts.Folder, true);
            }
        }
    }
}
