using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Blazor.Services;
using EasyAbp.AbpHelper.Gui.ModuleManagement.Explorer.Dtos;
using EasyAbp.AbpHelper.Gui.ModuleManagement.Explorer;
using EasyAbp.AbpHelper.Gui.ModuleManagement.Installer.Dtos;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.ModuleManagement.Components
{
    public partial class Explorer
    {
        protected InstallationActuatorModal InstallationActuatorModal;
        
        protected IReadOnlyList<ModuleGroupDto> ModuleGroups { get; set; } = new List<ModuleGroupDto>();
        
        protected Dictionary<string, List<string>> AppProjectsInstalledModuleNames = new();
        
        protected Dictionary<string, ModuleDto> ModuleNamesModuleMapping { get; set; }
        
        protected Dictionary<ModuleDto, ModuleGroupDto> ModuleModuleGroupMapping { get; set; }

        protected string SelectedTab { get; set; }

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
            await BuildModuleGroupsAsync();
            await BuildAppProjectsInstalledModuleNamesAsync();
            
            BuildModuleNamesModuleMapping();
            BuildModuleModuleGroupMapping();
            ChangeModuleCheckBoxesAccordingToInstalledModules();
            
            SelectedTab = ModuleGroups.FirstOrDefault()?.Id;
        }

        private async Task BuildModuleGroupsAsync()
        {
            ModuleGroups = (await _service.GetModuleGroupListAsync()).Items;
        }

        private async Task BuildAppProjectsInstalledModuleNamesAsync()
        {
            AppProjectsInstalledModuleNames = await _installedModulesLookupService.GetAsync(_currentSolution.Value);
        }

        private void ChangeModuleCheckBoxesAccordingToInstalledModules()
        {
            foreach (var (appLayerName, installedModuleNames) in AppProjectsInstalledModuleNames)
            {
                foreach (var module in from installedModuleName in installedModuleNames
                    where ModuleNamesModuleMapping.ContainsKey(installedModuleName)
                    select ModuleNamesModuleMapping[installedModuleName])
                {
                    module.Checked = true;
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
                        new KeyValuePair<string, ModuleDto>($"{moduleGroup.Id}.{module.Id}", module)).ToList());
        }
        
        private void ModuleChanged(bool value, ModuleDto module)
        {
            if (module.Checked != value)
            {
                module.Checked = value;
            }

            if (value)
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
                if (moduleGroup.Modules.All(x => !x.Checked))
                {
                    foreach (var module in moduleGroup.Modules.Where(x => x.Default))
                    {
                        module.Checked = true;
                        ModuleChanged(true, module);
                    }
                }
                else
                {
                    foreach (var module in moduleGroup.Modules)
                    {
                        module.Checked = true;
                        ModuleChanged(true, module);
                    }
                }
            }
            else
            {
                foreach (var module in moduleGroup.Modules)
                {
                    module.Checked = false;
                    ModuleChanged(false, module);
                }
            }
        }

        private List<AddManyModuleInput> GetAddManyModuleInputList()
        {
            var list = new List<AddManyModuleInput>();
            
            foreach (var moduleGroup in ModuleGroups)
            {
                var installationInfos = new List<AddManyModuleInputInstallationInfo>(moduleGroup.Modules
                    .Where(x => x.Checked).Select(module =>
                        new AddManyModuleInputInstallationInfo
                        {
                            ModuleGroupId = moduleGroup.Id,
                            ModuleId = module.Id,
                            Submodule = module.Submodule,
                            Targets = module.Targets
                                .Where(tar => !AppProjectsInstalledModuleNames[tar].Contains(module.Id)).ToList()
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

                    if (module.Checked && module.Targets.Contains(appLayerName))
                    {
                        continue;
                    }

                    var moduleGroup = ModuleModuleGroupMapping[module];

                    if (!moduleGroupRemoveManyModuleInputDictionary.ContainsKey(moduleGroup))
                    {
                        moduleGroupRemoveManyModuleInputDictionary[moduleGroup] = new RemoveManyModuleInput
                        {
                            DirectoryPath = _currentSolution.Value.DirectoryPath,
                            InstallationInfos = new List<RemoveManyModuleInputInstallationInfo>()
                        };
                    }

                    var installationInfos = moduleGroupRemoveManyModuleInputDictionary[moduleGroup].InstallationInfos;
                        
                    var existingInstallationInfo = installationInfos.FirstOrDefault(x =>
                        x.ModuleGroupId == moduleGroup.Id && x.ModuleId == module.Id);

                    if (existingInstallationInfo == null)
                    {
                        installationInfos.Add(new RemoveManyModuleInputInstallationInfo
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
    }
}
