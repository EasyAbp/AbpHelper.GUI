using EasyAbp.AbpHelper.Core;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Cli;
using Volo.Abp.Modularity;

namespace EasyAbp.AbpHelper.Gui
{
    [DependsOn(
        typeof(AbpAutoMapperModule),
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
    }
}
