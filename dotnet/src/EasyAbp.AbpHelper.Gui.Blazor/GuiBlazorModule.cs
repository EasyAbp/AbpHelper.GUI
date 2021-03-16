using System;
using System.Net.Http;
using Blazor.Analytics;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using EasyAbp.AbpHelper.Gui.Menus;
using EasyAbp.AbpHelper.Gui.Toolbars;
using IdentityModel;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Components.WebAssembly.BasicTheme;
using Volo.Abp.AspNetCore.Components.WebAssembly.BasicTheme.Themes.Basic;
using Volo.Abp.AspNetCore.Components.WebAssembly.Theming.Routing;
using Volo.Abp.Autofac.WebAssembly;
using Volo.Abp.Modularity;
using Volo.Abp.AspNetCore.Components.WebAssembly.Theming.Toolbars;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity.Blazor;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.AbpHelper.Gui
{
    [DependsOn(
        typeof(AbpAutofacWebAssemblyModule),
        typeof(GuiHttpApiClientModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpAspNetCoreComponentsWebAssemblyBasicThemeModule),
        typeof(AbpIdentityBlazorModule)
    )]
    public class GuiBlazorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var environment = context.Services.GetSingletonInstance<IWebAssemblyHostEnvironment>();
            var builder = context.Services.GetSingletonInstance<WebAssemblyHostBuilder>();

            ConfigureAuthentication(builder);
            ConfigureHttpClient(context, environment);
            ConfigureMenu(context);
            ConfigureToolbar(context);
            ConfigureBlazorise(context);
            ConfigureRouter(context);
            ConfigureUI(builder);
            ConfigureAutoMapper(context);
            ConfigureGoogleAnalytics(context);
        }

        private void ConfigureGoogleAnalytics(ServiceConfigurationContext context)
        {
            context.Services.AddGoogleAnalytics("G-7EJ58X64VW");
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
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();
        }

        private static void ConfigureAuthentication(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("AuthServer", options.ProviderOptions);
                options.UserOptions.RoleClaim = JwtClaimTypes.Role;
                options.ProviderOptions.DefaultScopes.Add("Gui");
                options.ProviderOptions.DefaultScopes.Add("role");
                options.ProviderOptions.DefaultScopes.Add("email");
                options.ProviderOptions.DefaultScopes.Add("phone");
            });
        }

        private static void ConfigureUI(WebAssemblyHostBuilder builder)
        {
            builder.RootComponents.Add<App>("#ApplicationContainer");
        }

        private static void ConfigureHttpClient(ServiceConfigurationContext context, IWebAssemblyHostEnvironment environment)
        {
            context.Services.AddTransient(sp => new HttpClient
            {
                BaseAddress = new Uri(environment.BaseAddress)
            });
        }

        private void ConfigureAutoMapper(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<GuiBlazorModule>();
            });
        }
    }
}
