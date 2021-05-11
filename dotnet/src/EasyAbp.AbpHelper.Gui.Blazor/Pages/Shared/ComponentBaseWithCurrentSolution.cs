using System;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Blazor.Services;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.Shared
{
    public abstract class ComponentBaseWithCurrentSolution : GuiComponentBase, IDisposable
    {
        [Inject]
        private ICurrentSolution CurrentSolution { get; set; }
        
        protected override void OnInitialized()
        {
            CurrentSolution.OnChangeAsync += CurrentSolutionChangedAsync;
        }

        public void Dispose()
        {
            CurrentSolution.OnChangeAsync -= CurrentSolutionChangedAsync;
        }
        
        protected virtual async Task CurrentSolutionChangedAsync()
        {
            await OnCurrentSolutionChangedAsync();
            
            StateHasChanged();
        }

        protected abstract Task OnCurrentSolutionChangedAsync();
    }
}
