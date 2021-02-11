using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.Autofac;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace EasyAbp.AbpHelper.Gui
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpTestBaseModule),
        typeof(AbpAuthorizationModule),
        typeof(GuiApplicationModule)
    )]
    public class GuiTestBaseModule : AbpModule
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
