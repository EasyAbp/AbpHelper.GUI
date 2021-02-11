using System;
using System.Collections.Generic;

namespace EasyAbp.AbpHelper.Gui.ModuleManagement.Installer.Dtos
{
    [Serializable]
    public class ModuleGroupDto
    {
        public string Id { get; set; }
        
        public string Org { get; set; }
        
        public string Repo { get; set; }
        
        public string Category { get; set; }
        
        public string Desc { get; set; }
        
        public List<string> Tags { get; set; }
        
        public List<ModuleDto> Modules { get; set; }
    }
}