using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Pages.LogService.Components;
using EasyAbp.AbpHelper.Gui.Pages.Solutions.Components;
using Volo.Abp.AspNetCore.Components.WebAssembly.BasicTheme.Themes.Basic;
using Volo.Abp.AspNetCore.Components.WebAssembly.Theming.Toolbars;

namespace EasyAbp.AbpHelper.Gui.Toolbars
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
            context.Toolbar.Items.RemoveAll(x => x.ComponentType == typeof(LoginDisplay));
            
            context.Toolbar.Items.Insert(0, new ToolbarItem(typeof(Logs)));
            
            context.Toolbar.Items.Add(new ToolbarItem(typeof(SolutionSwitch)));

            return Task.CompletedTask;
        }
    }
}