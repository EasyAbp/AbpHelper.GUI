using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Blazor.Services;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.Shared
{
    public abstract class ExecutableComponentBaseWithRunningPath<TInput> : ExecutableComponentBaseWithCurrentSolution where TInput : InputDtoWithRunningPath, new()
    {
        [Inject]
        protected ICurrentSolution CurrentSolution { get; set; }
        
        protected TInput Input { get; set; } = new();
        
        protected override Task OnInitializedAsync()
        {
            SetRunningPathToCurrentSolutionPath();
            
            return base.OnInitializedAsync();
        }

        protected override void OnCurrentSolutionChanged()
        {
            SetRunningPathToCurrentSolutionPath();
        }
        
        private void SetRunningPathToCurrentSolutionPath()
        {
            Input.RunningPath = CurrentSolution.Value?.DirectoryPath ?? string.Empty;
        }
    }
}