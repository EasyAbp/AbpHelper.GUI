using System;
using System.Collections.Generic;

namespace EasyAbp.AbpHelper.Gui.Solutions.Dtos
{
    [Serializable]
    public class GetPackageDictionaryOutput
    {
        public string SolutionName { get; set; }
        
        public Dictionary<string, List<PackageInfo>> Items { get; set; }
    }
}