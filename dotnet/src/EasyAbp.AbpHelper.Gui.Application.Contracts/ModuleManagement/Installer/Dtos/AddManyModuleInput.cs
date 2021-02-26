using System.Collections.Generic;

namespace EasyAbp.AbpHelper.Gui.ModuleManagement.Installer.Dtos
{
    public class AddManyModuleInput
    {
        public string DirectoryPath { get; set; }
        
        public string SpecifiedVersion { get; set; }
        
        public List<InstallationInfo> InstallationInfos { get; set; }
    }
}