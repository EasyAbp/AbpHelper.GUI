using System;

namespace EasyAbp.AbpHelper.Gui.ModuleManagement.Explorer.Dtos
{
    [Serializable]
    public class ModuleLibrarySourceDto
    {
        public string Name { get; set; }
        
        public string IndexUrl { get; set; }
    }
}