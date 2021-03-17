using System;

namespace EasyAbp.AbpHelper.Gui.UpdateCheck
{
    [Serializable]
    public class UpdateCheckCacheItem
    {
        public string LatestVersion { get; set; }
    }
}