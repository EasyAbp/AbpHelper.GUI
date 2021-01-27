using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.WebAssembly.BasicTheme.Themes.Basic;
using Volo.Abp.AspNetCore.Components.WebAssembly.Theming.Toolbars;

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
            context.Toolbar.Items.RemoveAll(x => x.ComponentType == typeof(LoginDisplay));
            
            return Task.CompletedTask;
        }
    }
}