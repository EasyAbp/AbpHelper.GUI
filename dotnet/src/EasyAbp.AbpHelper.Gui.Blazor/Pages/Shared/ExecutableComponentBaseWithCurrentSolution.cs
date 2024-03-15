using System;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Blazor.Services;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.Shared
{
    public abstract class ExecutableComponentBaseWithCurrentSolution : ExecutableComponentBase, IDisposable
    {
        [Inject]
        private ICurrentSolution CurrentSolution { get; set; }

        [Inject]
        private ICurrentTemplate CurrentTemplate { get; set; }

        protected override void OnInitialized()
        {
            CurrentSolution.OnChangeAsync += CurrentSolutionChangedAsync;
            CurrentTemplate.OnChangeAsync += CurrentTemplateChangedAsync;
        }

        public void Dispose()
        {
            CurrentSolution.OnChangeAsync -= CurrentSolutionChangedAsync;
            CurrentTemplate.OnChangeAsync -= CurrentTemplateChangedAsync;
        }
        
        protected virtual async Task CurrentSolutionChangedAsync()
        {
            await OnCurrentSolutionChangedAsync();
            
            StateHasChanged();
        }

        protected abstract Task OnCurrentSolutionChangedAsync();

        protected virtual async Task CurrentTemplateChangedAsync()
        {
            await OnCurrentTemplateChangedAsync();

            StateHasChanged();
        }

        protected abstract Task OnCurrentTemplateChangedAsync();
    }
}
