namespace EasyAbp.AbpHelper.Gui.Solutions.Dtos
{
    public class PackageInfo
    {
        public string Name { get; }

        public string Version { get; }

        public PackageInfo(string name, string version)
        {
            Name = name;
            Version = version;
        }
    }
}