using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Blazor.Services;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.Shared
{
    public abstract class ExecutableComponentBaseWithDirectory<TInput> : ExecutableComponentBaseWithCurrentSolution where TInput : InputDtoWithDirectory, new()
    {
        [Inject]
        protected ICurrentSolution CurrentSolution { get; set; }

        [Inject]
        protected ICurrentTemplate CurrentTemplate { get; set; }

        protected TInput Input { get; set; } = new();

        protected override Task OnInitializedAsync()
        {
            SetDirectoryToCurrentSolutionPath();
            SetDirectoryToCurrentTemplatePath();

            return base.OnInitializedAsync();
        }

        protected override Task OnCurrentSolutionChangedAsync()
        {
            SetDirectoryToCurrentSolutionPath();

            return Task.CompletedTask;
        }

        protected virtual void SetDirectoryToCurrentSolutionPath()
        {
            Input.Directory = CurrentSolution.Value?.DirectoryPath ?? string.Empty;
        }

        protected override Task OnCurrentTemplateChangedAsync()
        {
            SetDirectoryToCurrentTemplatePath();

            return Task.CompletedTask;
        }

        protected virtual void SetDirectoryToCurrentTemplatePath()
        {
            Input.TemplatePath = CurrentTemplate.Value?.DirectoryPath ?? string.Empty;
        }
    }
}