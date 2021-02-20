using System.Collections.Generic;
using System.Linq;
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
        private IReadOnlyList<SolutionDto> _recentlySolutions = new List<SolutionDto>();

        [Inject]
        private ICurrentSolution CurrentSolution { get; set; }
        
        [Inject]
        private ISolutionAppService Service { get; set; }
        
        private Modal _modal;

        private SolutionDto Solution { get; set; } = new()
        {
            SolutionType = SolutionType.Application
        };
        
        protected override async Task OnInitializedAsync()
        {
            await RefreshSolutions();
        }

        private async Task RefreshSolutions()
        {
            var solutions = await SolutionAppService.GetListAsync();

            _recentlySolutions = solutions.Items;

            CurrentSolution.Set(_recentlySolutions.Count > 0 ? _recentlySolutions[0] : null);
        }

        private async Task ChangeSolutionAsync(SolutionDto solutionDto)
        {
            await SolutionAppService.UseAsync(solutionDto);

            await RefreshSolutions();
        }

        private Task OpenOpenSolutionModalAsync()
        {
            _modal.Show();
            
            return Task.CompletedTask;
        }

        private void CloseOpenSolutionModal()
        {
            _modal.Hide();
        }
        
        private async Task OpenSolutionExecuteAsync()
        {
            await Service.UseAsync(Solution);
            
            _modal.Hide();

            await RefreshSolutions();
        }

        private async Task DeleteSolutionAsync(SolutionDto solution)
        {
            await Service.DeleteAsync(solution);
        }
    }
}