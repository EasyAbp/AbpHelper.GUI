using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Services;
using EasyAbp.AbpHelper.Gui.ModuleManagement.Explorer.Dtos;
using EasyAbp.AbpHelper.Gui.ModuleManagement.Explorer;
using EasyAbp.AbpHelper.Gui.ModuleManagement.Installer.Dtos;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.AbpHelper.Gui.Pages.ModuleManagement.Components
{
    public partial class Explorer
    {
        protected InstallationActuatorModal InstallationActuatorModal;
        
        protected IReadOnlyList<ModuleGroupDto> ModuleGroups { get; set; } = new List<ModuleGroupDto>();
        
        protected Dictionary<string, List<string>> AppProjectsInstalledModuleNames = new();
        
        protected Dictionary<string, ModuleDto> ModuleNamesModuleMapping { get; set; }
        
        protected Dictionary<ModuleDto, ModuleGroupDto> ModuleModuleGroupMapping { get; set; }

        protected string SelectedTab { get; set; }
        
        protected bool BuildingModulesData { get; set; }
        
        protected bool CanNotReset { get; set; }

        private readonly ICurrentSolution _currentSolution;
        private readonly IInstalledModulesLookupService _installedModulesLookupService;
        private readonly IModuleManagementExplorerAppService _service;
        
        public Explorer(
            ICurrentSolution currentSolution,
            IInstalledModulesLookupService installedModulesLookupService,
            IModuleManagementExplorerAppService service)
        {
            _currentSolution = currentSolution;
            _installedModulesLookupService = installedModulesLookupService;
            _service = service;
        }

        protected override async Task OnInitializedAsync()
        {
            await RebuildModulesDataAsync();

            SelectedTab = ModuleGroups.FirstOrDefault()?.Id;
        }

        protected override bool ShouldRender()
        {
            return !BuildingModulesData;
        }

        private async Task FilterModuleGroupsAsync(string value)
        {
            var selectedTabIsVisible = false;
            
            var hasValue = !value.IsNullOrWhiteSpace();
            
            foreach (var moduleGroup in ModuleGroups)
            {
                if (!hasValue || moduleGroup.Id.Contains(value, StringComparison.OrdinalIgnoreCase))
                {
                    moduleGroup.Visible = true;

                    if (moduleGroup.Id == SelectedTab)
                    {
                        selectedTabIsVisible = true;
                    }
                }
                else
                {
                    moduleGroup.Visible = false;
                }
            }

            if (!selectedTabIsVisible)
            {
                SelectedTab = null;
            }
        }

        private async Task RebuildModulesDataAsync()
        {
            BuildingModulesData = true;
            
            await BuildModuleGroupsAsync();
            await BuildAppProjectsInstalledModuleNamesAsync();
            
            BuildModuleNamesModuleMapping();
            BuildModuleModuleGroupMapping();
            ChangeModuleCheckBoxesAccordingToInstalledModules();

            BuildingModulesData = false;
            
            StateHasChanged();
        }

        private async Task ResetChangesAsync()
        {
            CanNotReset = true;
            StateHasChanged();

            await RebuildModulesDataAsync();

            CanNotReset = false;
        }

        private async Task BuildModuleGroupsAsync()
        {
            ModuleGroups = (await _service.GetModuleGroupListAsync()).Items;
        }

        private async Task BuildAppProjectsInstalledModuleNamesAsync()
        {
            AppProjectsInstalledModuleNames = _currentSolution.Value != null
                ? await _installedModulesLookupService.GetAsync(_currentSolution.Value)
                : new Dictionary<string, List<string>>();
        }

        private void ChangeModuleCheckBoxesAccordingToInstalledModules()
        {
            foreach (var (appLayerName, installedModuleNames) in AppProjectsInstalledModuleNames)
            {
                foreach (var module in from installedModuleName in installedModuleNames
                    where ModuleNamesModuleMapping.ContainsKey(installedModuleName)
                    select ModuleNamesModuleMapping[installedModuleName])
                {
                    module.Checked = false;
                    module.Indeterminate = true;
                    module.Installed = true;
                    module.Targets.AddIfNotContains(appLayerName);
                }
            }
        }

        private void BuildModuleModuleGroupMapping()
        {
            ModuleModuleGroupMapping = new Dictionary<ModuleDto, ModuleGroupDto>(ModuleGroups
                .SelectMany(moduleGroup => moduleGroup.Modules,
                    (moduleGroup, module) => new KeyValuePair<ModuleDto, ModuleGroupDto>(module, moduleGroup))
                .ToList());
        }

        private void BuildModuleNamesModuleMapping()
        {
            ModuleNamesModuleMapping = new Dictionary<string, ModuleDto>(ModuleGroups
                .SelectMany(moduleGroup => moduleGroup.Modules,
                    (moduleGroup, module) =>
                        new KeyValuePair<string, ModuleDto>(GetModulePackageName(moduleGroup.Id, module.Id), module)).ToList());
        }
        
        private void ModuleChanged(bool value, ModuleDto module, bool forceCheck = false)
        {
            if (value)
            {
                if (forceCheck || module.Indeterminate || !module.Installed)
                {
                    module.Checked = true;
                    module.Indeterminate = false;
                }
                else if (!module.Checked)
                {
                    module.Indeterminate = true;
                }
            }
            else
            {
                module.Checked = false;
                module.Indeterminate = false;
            }

            if (module.Checked)
            {
                if (module.Targets.IsNullOrEmpty())
                {
                    module.Targets = module.DefaultTargets;
                }
            }
        }

        private void ModulesAllChanged(bool value, ModuleGroupDto moduleGroup)
        {
            if (value)
            {
                if (moduleGroup.Modules.All(x => !x.Checked && !x.Indeterminate))
                {
                    foreach (var module in moduleGroup.Modules.Where(x =>
                        x.Default && x.DefaultTargets.Any(y => AppProjectsInstalledModuleNames.ContainsKey(y))))
                    {
                        ModuleChanged(true, module, true);
                    }
                }
                else
                {
                    foreach (var module in moduleGroup.Modules)
                    {
                        ModuleChanged(true, module, true);
                    }
                }
            }
            else
            {
                foreach (var module in moduleGroup.Modules)
                {
                    ModuleChanged(false, module, true);
                }
            }
        }

        private static string GetModulePackageName(string moduleGroupId, string moduleId)
        {
            return moduleId != "" ? $"{moduleGroupId}.{moduleId}" : moduleGroupId;
        }

        private List<AddManyModuleInput> GetAddManyModuleInputList()
        {
            var list = new List<AddManyModuleInput>();
            
            foreach (var moduleGroup in ModuleGroups)
            {
                var installationInfos = new List<InstallationInfo>(moduleGroup.Modules
                    .Where(x => x.Checked).Select(module =>
                        new InstallationInfo
                        {
                            ModuleGroupId = moduleGroup.Id,
                            ModuleId = module.Id,
                            Submodule = module.Submodule,
                            Targets = module.Targets.Where(tar =>
                                AppProjectsInstalledModuleNames.ContainsKey(tar) &&
                                !AppProjectsInstalledModuleNames[tar]
                                    .Contains(GetModulePackageName(moduleGroup.Id, module.Id))).ToList()
                        }
                    )
                );
                
                list.Add(new AddManyModuleInput
                {
                    DirectoryPath = _currentSolution.Value.DirectoryPath,
                    InstallationInfos = installationInfos
                });
            }

            return list;
        }
        
        private List<RemoveManyModuleInput> GetRemoveManyModuleInputList()
        {
            var moduleGroupRemoveManyModuleInputDictionary = new Dictionary<ModuleGroupDto, RemoveManyModuleInput>();
            
            foreach (var (appLayerName, installedModuleNames) in AppProjectsInstalledModuleNames)
            {
                foreach (var moduleName in installedModuleNames)
                {
                    if (!ModuleNamesModuleMapping.ContainsKey(moduleName))
                    {
                        continue;
                    }

                    var module = ModuleNamesModuleMapping[moduleName];

                    if ((module.Checked || module.Indeterminate) && module.Targets.Contains(appLayerName))
                    {
                        continue;
                    }

                    var moduleGroup = ModuleModuleGroupMapping[module];

                    if (!moduleGroupRemoveManyModuleInputDictionary.ContainsKey(moduleGroup))
                    {
                        moduleGroupRemoveManyModuleInputDictionary[moduleGroup] = new RemoveManyModuleInput
                        {
                            DirectoryPath = _currentSolution.Value.DirectoryPath,
                            InstallationInfos = new List<InstallationInfo>()
                        };
                    }

                    var installationInfos = moduleGroupRemoveManyModuleInputDictionary[moduleGroup].InstallationInfos;
                        
                    var existingInstallationInfo = installationInfos.FirstOrDefault(x =>
                        x.ModuleGroupId == moduleGroup.Id && x.ModuleId == module.Id);

                    if (existingInstallationInfo == null)
                    {
                        installationInfos.Add(new InstallationInfo
                        {
                            ModuleGroupId = moduleGroup.Id,
                            ModuleId = module.Id,
                            Submodule = module.Submodule,
                            Targets = new List<string> {appLayerName}
                        });
                    }
                    else
                    {
                        existingInstallationInfo.Targets.AddIfNotContains(appLayerName);
                    }
                }
            }

            return moduleGroupRemoveManyModuleInputDictionary.Values.ToList();
        }

        protected override async Task OnCurrentSolutionChangedAsync()
        {
            await RebuildModulesDataAsync();
        }
    }
}
