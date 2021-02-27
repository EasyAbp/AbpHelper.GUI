using System;
using System.Collections.Generic;

namespace EasyAbp.AbpHelper.Gui.ModuleManagement.Explorer.Dtos
{
    [Serializable]
    public class ModuleGroupDto : IComparable
    {
        public string Id { get; set; }
        
        public string Org { get; set; }
        
        public string Repo { get; set; }
        
        public string Category { get; set; }
        
        public string Desc { get; set; }
        
        public List<string> Tags { get; set; }
        
        public List<ModuleDto> Modules { get; set; }
        
        public int CompareTo(object obj)
        {
            return obj is not ModuleGroupDto dto ? 1 : string.Compare(Id, dto.Id, StringComparison.Ordinal);
        }
    }
}