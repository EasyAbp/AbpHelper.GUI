using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.New;
using EasyAbp.AbpHelper.Gui.AbpCli.New.Dtos;
using EasyAbp.AbpHelper.Gui.AbpCli.Update;
using EasyAbp.AbpHelper.Gui.AbpCli.Update.Dtos;
using EasyAbp.AbpHelper.Gui.Blazor.Services;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.AbpCli.Components.Update
{
    public partial class Update
    {
        [Inject]
        private IAbpCliUpdateAppService Service { get; set; }
        
        [Inject]
        private ICurrentSolution CurrentSolution { get; set; }

        protected AbpUpdateInput Input { get; set; } = new();

        protected override async Task InternalExecuteAsync()
        {
            await Service.UpdateAsync(Input);
        }

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
