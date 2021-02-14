using System.Collections.Generic;

namespace EasyAbp.AbpHelper.Gui.ModuleManagement.Installer.Dtos
{
    public class AddManyModuleInput
    {
        public string DirectoryPath { get; set; }
        
        public string SpecifiedVersion { get; set; }
        
        public List<AddManyModuleInputInstallationInfo> InstallationInfos { get; set; }
    }

    public class AddManyModuleInputInstallationInfo
    {
        public string ModuleGroupId { get; set; }
        
        public string ModuleId { get; set; }

        public string Submodule { get; set; }
        
        public IEnumerable<string> Targets { get; set; }
    }
}