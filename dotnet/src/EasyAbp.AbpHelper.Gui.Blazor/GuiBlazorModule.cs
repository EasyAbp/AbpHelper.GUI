using System;
using System.IO;
using System.Net.Http;
using Blazorx.Analytics;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using EasyAbp.AbpHelper.Gui.Blazor.Menus;
using EasyAbp.AbpHelper.Gui.Blazor.Toolbars;
using EasyAbp.AbpHelper.Gui.Localization;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.Components.Server.LeptonXLiteTheme;
using Volo.Abp.AspNetCore.Components.Server.LeptonXLiteTheme.Bundling;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AspNetCore.Components.Web.Theming.Toolbars;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic.Bundling;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Json;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement.Blazor.Server;
using Volo.Abp.Swashbuckle;
using Volo.Abp.UI.Navigation;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.AbpHelper.Gui.Blazor
{
    [DependsOn(
        typeof(GuiApplicationModule),
        typeof(GuiHttpApiModule),
        typeof(AbpAspNetCoreMvcUiBasicThemeModule),
        typeof(AbpAutofacModule),
        typeof(AbpSwashbuckleModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpAspNetCoreComponentsServerLeptonXLiteThemeModule),
        typeof(AbpSettingManagementBlazorServerModule)
    )]
    public class GuiBlazorModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<AbpJsonOptions>(options =>
            {
                options.UseHybridSerializer = false;
            });
            
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(
                    typeof(GuiResource),
                    typeof(GuiApplicationModule).Assembly,
                    typeof(GuiApplicationContractsModule).Assembly,
                    typeof(GuiBlazorModule).Assembly
                );
                
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            ConfigureUrls(configuration);
            ConfigureBundles();
            // ConfigureAuthentication(context, configuration);
            ConfigureAutoMapper();
            ConfigureVirtualFileSystem(hostingEnvironment);
            ConfigureLocalizationServices();
            ConfigureSwaggerServices(context.Services);
            ConfigureAutoApiControllers();
            ConfigureHttpClient(context);
            ConfigureBlazorise(context);
            ConfigureRouter(context);
            ConfigureMenu(context);
            ConfigureToolbar(context);
            ConfigureGoogleAnalytics(context);
        }

        private void ConfigureGoogleAnalytics(ServiceConfigurationContext context)
        {
            context.Services.AddGoogleAnalytics("G-7EJ58X64VW");
        }
        
        private void ConfigureUrls(IConfiguration configuration)
        {
            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
                options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"].Split(','));
            });
        }
        
        private void ConfigureBundles()
        {
            Configure<AbpBundlingOptions>(options =>
            {
                // MVC UI
                options.StyleBundles.Configure(
                    BasicThemeBundles.Styles.Global,
                    bundle =>
                    {
                        bundle.AddFiles("/global-styles.css");
                    }
                );

                //BLAZOR UI
                options.StyleBundles.Configure(
                    BlazorLeptonXLiteThemeBundles.Styles.Global,
                    bundle =>
                    {
                        bundle.AddFiles("/blazor-global-styles.css");
                        //You can remove the following line if you don't use Blazor CSS isolation for components
                        bundle.AddFiles("/EasyAbp.AbpHelper.Gui.Blazor.styles.css");
                    }
                );
            });
        }
        
                private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<GuiApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}EasyAbp.AbpHelper.Gui.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<GuiApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}EasyAbp.AbpHelper.Gui.Application"));
                    options.FileSets.ReplaceEmbeddedByPhysical<GuiBlazorModule>(hostingEnvironment.ContentRootPath);
                });
            }
        }

        private void ConfigureLocalizationServices()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<AbpUiResource>()
                    .AddVirtualJson("/Localization/AbpUi");
                
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            });
        }

        private void ConfigureSwaggerServices(IServiceCollection services)
        {
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Gui API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                }
            );
        }
        
        private void ConfigureAutoApiControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(GuiApplicationModule).Assembly);
            });
        }

        private void ConfigureMenu(ServiceConfigurationContext context)
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new GuiMenuContributor(context.Services.GetConfiguration()));
            });
        }
        
        private void ConfigureToolbar(ServiceConfigurationContext context)
        {
            Configure<AbpToolbarOptions>(options =>
            {
                options.Contributors.Add(new GuiToolbarContributor());
            });
        }

        private void ConfigureRouter(ServiceConfigurationContext context)
        {
            Configure<AbpRouterOptions>(options =>
            {
                options.AppAssembly = typeof(GuiBlazorModule).Assembly;
            });
        }

        private void ConfigureBlazorise(ServiceConfigurationContext context)
        {
            context.Services
                .AddBootstrap5Providers()
                .AddFontAwesomeIcons();
        }

        private static void ConfigureHttpClient(ServiceConfigurationContext context)
        {
            context.Services.AddTransient(sp => new HttpClient
            {
                BaseAddress = new Uri("/")
            });
        }

        private void ConfigureAutoMapper()
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<GuiBlazorModule>();
            });
        }
        
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var env = context.GetEnvironment();
            var app = context.GetApplicationBuilder();

            app.UseAbpRequestLocalization();

            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseCorrelationId();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSwagger();
            app.UseAbpSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "AbpHelper GUI API");
            });
            app.UseConfiguredEndpoints();
        }
    }
}
