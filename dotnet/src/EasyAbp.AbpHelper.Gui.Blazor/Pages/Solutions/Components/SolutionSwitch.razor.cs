using System.Collections.Generic;
using System.Threading.Tasks;
using Blazorise;
using EasyAbp.AbpHelper.Gui.Blazor.Services;
using EasyAbp.AbpHelper.Gui.Solutions;
using EasyAbp.AbpHelper.Gui.Solutions.Dtos;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.Solutions.Components
{
    public partial class SolutionSwitch
    {
        private Task RedirectToSolutionsPageAsync()
        {
            NavigationManager.NavigateTo("/Solutions");
            
            return Task.CompletedTask;
        }
    }
}