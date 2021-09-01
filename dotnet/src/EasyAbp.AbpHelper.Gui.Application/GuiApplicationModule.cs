using EasyAbp.AbpHelper.Core;
using EasyAbp.AbpHelper.Gui.LogService;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Cli;
using Volo.Abp.Modularity;

namespace EasyAbp.AbpHelper.Gui
{
    [DependsOn(
        typeof(AbpAutoMapperModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpCliCoreModule),
        typeof(AbpHelperCoreModule),
        typeof(GuiApplicationContractsModule)
    )]
    public class GuiApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClient();
            
            context.Services.AddAutoMapperObjectMapper<GuiApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<GuiApplicationModule>(validate: true);
            });
        }
        
        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            context.ServiceProvider.GetRequiredService<ILogFilePathProvider>();
        }
    }
}
