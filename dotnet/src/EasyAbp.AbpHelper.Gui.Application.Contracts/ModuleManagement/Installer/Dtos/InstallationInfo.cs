using System.Collections.Generic;

namespace EasyAbp.AbpHelper.Gui.ModuleManagement.Installer.Dtos
{
    public class InstallationInfo
    {
        public string ModuleGroupId { get; set; }
        
        public string ModuleId { get; set; }
        
        public string Submodule { get; set; }

        public List<string> Targets { get; set; }
    }
}