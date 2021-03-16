using System.IO;

namespace EasyAbp.AbpHelper.Gui
{
    public static class StringExtensions
    {
        public static string SmartPathCombine(this string path1, string path2)
        {
            if (path1.Contains("\\"))
            {
                return $"{path1}\\{path2}";
            }

            if (path1.Contains("/"))
            {
                return $"{path1}/{path2}";
            }

            return Path.Combine(path1, path2);
        }
    }
}