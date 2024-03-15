using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.Templates.Components
{
    public partial class TemplateSwitch
    {
        private readonly NavigationManager _navigationManager;

        public TemplateSwitch(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }
        
        private Task RedirectToTemplatesPageAsync()
        {
            _navigationManager.NavigateTo("/Templates");
            
            return Task.CompletedTask;
        }
    }
}