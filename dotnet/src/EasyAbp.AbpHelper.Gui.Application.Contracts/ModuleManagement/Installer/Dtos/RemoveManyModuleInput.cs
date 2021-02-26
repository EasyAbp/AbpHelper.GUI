using System.Collections.Generic;

namespace EasyAbp.AbpHelper.Gui.ModuleManagement.Installer.Dtos
{
    public class RemoveManyModuleInput
    {
        public string DirectoryPath { get; set; }
        
        public List<InstallationInfo> InstallationInfos { get; set; }
    }
}