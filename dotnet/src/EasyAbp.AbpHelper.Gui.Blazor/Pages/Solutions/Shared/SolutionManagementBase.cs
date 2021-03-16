using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazorise;
using EasyAbp.AbpHelper.Gui.Services;
using EasyAbp.AbpHelper.Gui.Localization;
using EasyAbp.AbpHelper.Gui.Solutions;
using EasyAbp.AbpHelper.Gui.Solutions.Dtos;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.WebAssembly;

namespace EasyAbp.AbpHelper.Gui.Pages.Solutions.Shared
{
    public class SolutionManagementBase : GuiComponentBase, IDisposable
    {
        protected IReadOnlyList<SolutionDto> Solutions = new List<SolutionDto>();

        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        
        [Inject]
        protected ICurrentSolution CurrentSolution { get; set; }
        
        [Inject]
        protected ISolutionAppService Service { get; set; }
        
        [Inject]
        protected AbpBlazorMessageLocalizerHelper<GuiResource> Lh { get; set; }
        
        [Inject]
        protected ISolutionAppService SolutionAppService { get; set; }

        protected Modal Modal;

        protected virtual SolutionDto CreateSolution { get; set; } = new()
        {
            SolutionType = SolutionType.Application
        };
        
        protected override async Task OnInitializedAsync()
        {
            await RefreshSolutions();
        }

        protected virtual async Task RefreshSolutions()
        {
            var solutions = await SolutionAppService.GetListAsync();

            Solutions = solutions.Items;

            var targetSolution = Solutions.Count > 0 ? Solutions[0] : null;

            if (targetSolution == null && CurrentSolution.Value != null || 
                targetSolution != null && !targetSolution.Equals(CurrentSolution.Value))
            {
                await CurrentSolution.SetAsync(targetSolution);
            }
        }

        protected virtual async Task ChangeSolutionAsync(SolutionDto solutionDto)
        {
            try
            {
                await SolutionAppService.UseAsync(solutionDto);
            }
            finally
            {
                await RefreshSolutions();
            }
        }

        protected virtual Task OpenOpenSolutionModalAsync()
        {
            CreateSolution = new SolutionDto
            {
                SolutionType = SolutionType.Application
            };
            
            Modal.Show();
            
            return Task.CompletedTask;
        }

        protected virtual void CloseOpenSolutionModal()
        {
            Modal.Hide();
        }
        
        protected virtual async Task OpenSolutionExecuteAsync()
        {
            await Service.UseAsync(CreateSolution);
            
            Modal.Hide();

            await RefreshSolutions();
        }

        protected virtual async Task DeleteSolutionAsync(SolutionDto solution)
        {
            await Service.DeleteAsync(solution);
            
            await RefreshSolutions();
        }
        
        protected override void OnInitialized()
        {
            CurrentSolution.OnChangeAsync += StateHasChangedAsync;
        }

        public void Dispose()
        {
            CurrentSolution.OnChangeAsync -= StateHasChangedAsync;
        }

        protected virtual Task StateHasChangedAsync()
        {
            StateHasChanged();
            
            return Task.CompletedTask;
        }
    }
}