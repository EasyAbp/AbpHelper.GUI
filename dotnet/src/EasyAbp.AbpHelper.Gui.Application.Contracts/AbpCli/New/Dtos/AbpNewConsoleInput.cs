using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.New.Dtos
{
    public class AbpNewConsoleInput : AbpNewInputBase
    {
        public override string Template => "console";

        public AbpNewConsoleInput()
        {
        }

        public AbpNewConsoleInput([NotNull] string solutionName, [NotNull] string outputFolder,
            [CanBeNull] string version, bool preview, [CanBeNull] string templateSource, bool createSolutionFolder,
            [CanBeNull] string connectionString, Database databaseManagementSystem,
            [CanBeNull] string localFrameworkRef, bool noRandomPort, bool skipInstallingLibs, bool skipCache,
            bool withPublicWebsite) : base(solutionName, outputFolder, version, preview, templateSource,
            createSolutionFolder, connectionString, databaseManagementSystem, localFrameworkRef, noRandomPort,
            skipInstallingLibs, skipCache, withPublicWebsite)
        {
        }
    }
}