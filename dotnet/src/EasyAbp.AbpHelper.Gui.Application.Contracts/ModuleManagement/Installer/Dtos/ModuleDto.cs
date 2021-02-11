using System;
using System.Collections.Generic;

namespace EasyAbp.AbpHelper.Gui.ModuleManagement.Installer.Dtos
{
    [Serializable]
    public class ModuleDto
    {
        public string Id { get; set; }
        
        public List<string> Targets { get; set; }
        
        public bool Checked { get; set; }
    }
}