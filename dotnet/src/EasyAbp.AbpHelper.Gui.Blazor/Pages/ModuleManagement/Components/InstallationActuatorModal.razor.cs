using System.Collections.Generic;
using System.Threading.Tasks;
using Blazorise;
using EasyAbp.AbpHelper.Gui.ModuleManagement.Installer;
using EasyAbp.AbpHelper.Gui.ModuleManagement.Installer.Dtos;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.ModuleManagement.Components
{
    public partial class InstallationActuatorModal
    {
        [Inject]
        private IModuleManagementInstallerAppService Service { get; set; }
        
        private Modal _modal;
        
        private bool Executing { get; set; }

        private List<AddManyModuleInput> AddInputList { get; set; } = new();
        private List<RemoveManyModuleInput> RemoveInputList { get; set; } = new();

        public Task OpenAsync(List<AddManyModuleInput> addInputList, List<RemoveManyModuleInput> removeInputList)
        {
            AddInputList = addInputList;
            RemoveInputList = removeInputList;
            
            _modal.Show();
            
            return Task.CompletedTask;
        }

        private void CloseModal()
        {
            _modal.Hide();
        }
        
        protected override async Task InternalExecuteAsync()
        {
            Executing = true;
            
            foreach (var addManyModuleInput in AddInputList)
            {
                await Service.AddManyAsync(addManyModuleInput);
            }

            foreach (var removeManyModuleInput in RemoveInputList)
            {
                await Service.RemoveManyAsync(removeManyModuleInput);
            }

            Executing = false;
            
            _modal.Hide();
        }
    }
}