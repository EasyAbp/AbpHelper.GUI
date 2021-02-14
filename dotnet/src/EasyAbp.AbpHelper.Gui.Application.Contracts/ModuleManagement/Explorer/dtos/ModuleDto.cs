using System;
using System.Collections.Generic;

namespace EasyAbp.AbpHelper.Gui.ModuleManagement.Explorer.dtos
{
    [Serializable]
    public class ModuleDto
    {
        public string Id { get; set; }
        
        public string Submodule { get; set; }

        public List<string> Targets { get; set; }
        
        public bool Checked { get; set; }
    }
}