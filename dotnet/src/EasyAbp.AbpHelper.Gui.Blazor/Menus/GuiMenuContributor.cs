using System;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account.Localization;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;

namespace EasyAbp.AbpHelper.Gui.Blazor.Menus
{
    public class GuiMenuContributor : IMenuContributor
    {
        private readonly IConfiguration _configuration;

        public GuiMenuContributor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
            else if (context.Menu.Name == StandardMenus.User)
            {
                await ConfigureUserMenuAsync(context);
            }
        }

        private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var l = context.GetLocalizer<GuiResource>();

            context.Menu.Items.Add(new ApplicationMenuItem(
                    GuiMenus.AbpCli,
                    l["Menu:Home"],
                    "/",
                    "fa fa-home"
                )
            );
            
            context.Menu.Items.Add(new ApplicationMenuItem(
                    GuiMenus.AbpCli,
                    l["Menu:AbpCli"],
                    "/AbpCli",
                    "fa fa-television"
                )
            );
            
            context.Menu.Items.Add(new ApplicationMenuItem(
                    GuiMenus.CodeGeneration,
                    l["Menu:CodeGeneration"],
                    "/CodeGeneration",
                    "fa fa-code"
                )
            );

            context.Menu.Items.Add(new ApplicationMenuItem(
                    GuiMenus.ModuleManagement,
                    l["Menu:ModuleManagement"],
                    "/ModuleManagement",
                    "fa fa-cubes"
                )
            );

            return Task.CompletedTask;
        }

        private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
        {
            var accountStringLocalizer = context.GetLocalizer<AccountResource>();
            var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();

            var identityServerUrl = _configuration["AuthServer:Authority"] ?? "";

            if (currentUser.IsAuthenticated)
            {
                context.Menu.AddItem(new ApplicationMenuItem(
                    "Account.Manage",
                    accountStringLocalizer["ManageYourProfile"],
                    $"{identityServerUrl.EnsureEndsWith('/')}Account/Manage",
                    icon: "fa fa-cog",
                    order: 1000,
                    null,
                    "_blank"));
            }

            return Task.CompletedTask;
        }
    }
}
