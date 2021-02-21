using System;
using EasyAbp.AbpHelper.Gui.Blazor.Services;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.Shared
{
    public abstract class ExecutableComponentBaseWithCurrentSolution : ExecutableComponentBase, IDisposable
    {
        [Inject]
        private ICurrentSolution CurrentSolution { get; set; }
        
        protected override void OnInitialized()
        {
            CurrentSolution.OnChange += CurrentSolutionChanged;
        }

        public void Dispose()
        {
            CurrentSolution.OnChange -= CurrentSolutionChanged;
        }
        
        protected virtual void CurrentSolutionChanged()
        {
            OnCurrentSolutionChanged();
            
            StateHasChanged();
        }

        protected abstract void OnCurrentSolutionChanged();
    }
}
