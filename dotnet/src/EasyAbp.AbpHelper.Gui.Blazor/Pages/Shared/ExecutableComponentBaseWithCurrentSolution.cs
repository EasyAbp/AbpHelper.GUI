using System;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Services;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Pages.Shared
{
    public abstract class ExecutableComponentBaseWithCurrentSolution : ExecutableComponentBase, IDisposable
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
