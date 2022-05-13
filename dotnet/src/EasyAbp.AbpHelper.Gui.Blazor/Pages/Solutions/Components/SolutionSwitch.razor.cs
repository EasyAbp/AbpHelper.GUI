using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.Solutions.Components
{
    public partial class SolutionSwitch
    {
        private readonly NavigationManager _navigationManager;

        public SolutionSwitch(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }
        
        private Task RedirectToSolutionsPageAsync()
        {
            _navigationManager.NavigateTo("/Solutions");
            
            return Task.CompletedTask;
        }
    }
}