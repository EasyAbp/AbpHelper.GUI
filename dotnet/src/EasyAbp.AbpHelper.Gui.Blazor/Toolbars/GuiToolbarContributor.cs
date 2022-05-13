using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Blazor.Pages.LogService.Components;
using EasyAbp.AbpHelper.Gui.Blazor.Pages.Solutions.Components;
using Volo.Abp.AspNetCore.Components.Server.LeptonXLiteTheme.Themes.LeptonXLite.Toolbar;
using Volo.Abp.AspNetCore.Components.Web.Theming.Toolbars;

namespace EasyAbp.AbpHelper.Gui.Blazor.Toolbars
{
    public class GuiToolbarContributor : IToolbarContributor
    {
        public Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
        {
            if (context.Toolbar.Name != StandardToolbars.Main)
            {
                return Task.CompletedTask;
            }
            
            // Remove the login item.
            context.Toolbar.Items.RemoveAll(x => x.ComponentType == typeof(UserMenuComponent));
            
            context.Toolbar.Items.Insert(0, new ToolbarItem(typeof(Logs)));
            
            context.Toolbar.Items.Add(new ToolbarItem(typeof(SolutionSwitch)));

            return Task.CompletedTask;
        }
    }
}