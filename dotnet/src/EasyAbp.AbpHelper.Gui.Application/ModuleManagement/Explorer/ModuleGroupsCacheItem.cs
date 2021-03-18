using System;
using System.Collections.Generic;
using EasyAbp.AbpHelper.Gui.ModuleManagement.Explorer.Dtos;

namespace EasyAbp.AbpHelper.Gui.ModuleManagement.Explorer
{
    [Serializable]
    public class ModuleGroupsCacheItem
    {
        public List<ModuleGroupDto> ModuleGroupList { get; set; }
    }
}