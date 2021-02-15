using System;
using System.Collections.Generic;

namespace EasyAbp.AbpHelper.Gui.ModuleManagement.Explorer.Dtos
{
    [Serializable]
    public class ModuleLibraryIndexDto
    {
        public List<ModuleGroupDto> Groups { get; set; }
    }
}