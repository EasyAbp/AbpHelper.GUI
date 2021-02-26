using System.IO;

namespace EasyAbp.AbpHelper.Gui
{
    public static class StringExtensions
    {
        public static string[] SplitBySpace(this string str)
        {
            return str.Split(" ");
        }
    }
}