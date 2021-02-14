using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Core.Commands.Module.Add;
using EasyAbp.AbpHelper.Core.Commands.Module.Remove;
using EasyAbp.AbpHelper.Gui.ModuleManagement.Installer.Dtos;

namespace EasyAbp.AbpHelper.Gui.ModuleManagement.Installer
{
    public class ModuleManagementInstallerAppService : ModuleManagementAppService, IModuleManagementInstallerAppService
    {
        private readonly IDictionary<string, string> _projectShortNameToProjectNameMapping = new Dictionary<string, string>
        {
            {"s", "Domain.Shared"},
            {"o", "Domain"},
            {"e", "EntityFrameworkCore"},
            {"m", "MongoDB"},
            {"c", "Application.Contracts"},
            {"a", "Application"},
            {"h", "HttpApi"},
            {"l", "HttpApi.Client"},
            {"w", "Web"},
        };
        
        private readonly AddCommand _addCommand;
        private readonly RemoveCommand _removeCommand;

        public ModuleManagementInstallerAppService(
            AddCommand addCommand,
            RemoveCommand removeCommand)
        {
            _addCommand = addCommand;
            _removeCommand = removeCommand;
        }
        
        public virtual async Task AddManyAsync(AddManyModuleInput input)
        {
            foreach (var grouping in input.InstallationInfos.GroupBy(x => x.ModuleGroupId))
            {
                var custom = grouping
                    .Select(x => x.Targets.Select(tar => x.Submodule.IsNullOrWhiteSpace()
                        ? $"{x.ModuleId}:{GetProjectName(tar)}"
                        : $"{x.ModuleId}:{GetProjectName(tar)}:{x.Submodule}").JoinAsString(","))
                    .JoinAsString(",");

                await _addCommand.RunCommand(new AddCommandOption
                {
                    Directory = input.DirectoryPath,
                    ModuleName = grouping.Key,
                    Version = input.SpecifiedVersion,
                    Custom = custom
                });
            }
        }
        
        public virtual async Task RemoveManyAsync(RemoveManyModuleInput input)
        {
            foreach (var grouping in input.InstallationInfos.GroupBy(x => x.ModuleGroupId))
            {
                var custom = grouping
                    .Select(x => x.Targets.Select(tar => x.Submodule.IsNullOrWhiteSpace()
                        ? $"{x.ModuleId}:{GetProjectName(tar)}"
                        : $"{x.ModuleId}:{GetProjectName(tar)}:{x.Submodule}").JoinAsString(","))
                    .JoinAsString(",");

                await _removeCommand.RunCommand(new RemoveCommandOption
                {
                    Directory = input.DirectoryPath,
                    ModuleName = grouping.Key,
                    Custom = custom
                });
            }
        }

        protected virtual string GetProjectName(string projectShortName)
        {
            return _projectShortNameToProjectNameMapping.ContainsKey(projectShortName)
                ? _projectShortNameToProjectNameMapping[projectShortName]
                : projectShortName;
        }
    }
}