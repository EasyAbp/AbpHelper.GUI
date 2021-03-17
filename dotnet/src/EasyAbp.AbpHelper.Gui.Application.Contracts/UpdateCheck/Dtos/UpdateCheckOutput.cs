using System;

namespace EasyAbp.AbpHelper.Gui.UpdateCheck.Dtos
{
    [Serializable]
    public class UpdateCheckOutput
    {
        public string LatestVersion { get; set; }
        
        public string CurrentVersion { get; set; }

        public bool ShouldUpdate => LatestVersion != CurrentVersion;
    }
}