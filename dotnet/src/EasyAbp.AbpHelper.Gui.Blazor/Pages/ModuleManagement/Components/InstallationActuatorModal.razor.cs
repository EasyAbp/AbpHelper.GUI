using System;
using System.Collections.Generic;
using System.Linq;
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
        
        private Modal _actuatorModal;
        
        private Modal _editTargetModal;
        
        private bool Executing { get; set; }
        
        private string EditingTargetString { get; set; }
        
        private InstallationInfo EditingInstallationInfo { get; set; }

        private List<AddManyModuleInput> AddInputList { get; set; } = new();
        private List<RemoveManyModuleInput> RemoveInputList { get; set; } = new();
        
        private Func<Task> CallbackAfterExecution { get; set; }

        public Task OpenAsync(List<AddManyModuleInput> addInputList, List<RemoveManyModuleInput> removeInputList,
            Func<Task> callbackAfterExecution = null)
        {
            AddInputList = addInputList;
            RemoveInputList = removeInputList;
            CallbackAfterExecution = callbackAfterExecution;

            _actuatorModal.Show();

            return Task.CompletedTask;
        }

        private Task OpenEditTargetModalAsync(InstallationInfo installationInfo)
        {
            EditingInstallationInfo = installationInfo;
            EditingTargetString = EditingInstallationInfo.Targets.JoinAsString(",");

            _editTargetModal.Show();
            
            return Task.CompletedTask;
        }

        private Task SaveTargetAsync()
        {
            EditingInstallationInfo.Targets = EditingTargetString.Split(",").ToList();
            
            _editTargetModal.Hide();

            return Task.CompletedTask;
        }

        private void CloseEditTargetModal()
        {
            _editTargetModal.Hide();
        }
        
        private void CloseModal()
        {
            _actuatorModal.Hide();
        }

        public override async Task ExecuteAsync()
        {
            if (AddInputList.SelectMany(x => x.InstallationInfos).Any(x => !x.Targets.Any()) ||
                RemoveInputList.SelectMany(x => x.InstallationInfos).Any(x => !x.Targets.Any()))
            {
                await UiMessageService.Error(L["TargetsShouldNotBeEmpty"].Value);
                return;
            }
            
            await base.ExecuteAsync();
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

            await CallbackAfterExecution();

            Executing = false;
            
            _actuatorModal.Hide();
        }
    }
}