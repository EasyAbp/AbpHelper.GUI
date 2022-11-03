using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.New.Dtos
{
    public class AbpNewMauiInput : AbpNewInputBase
    {
        public override string Template => "maui";

        public AbpNewMauiInput()
        {
        }

        public AbpNewMauiInput([NotNull] string solutionName, [NotNull] string outputFolder,
            [CanBeNull] string version, bool preview, [CanBeNull] string templateSource, bool createSolutionFolder,
            [CanBeNull] string connectionString, Database databaseManagementSystem,
            [CanBeNull] string localFrameworkRef, bool noRandomPort, bool skipInstallingLibs) : base(solutionName,
            outputFolder, version, preview, templateSource, createSolutionFolder, connectionString,
            databaseManagementSystem, localFrameworkRef, noRandomPort, skipInstallingLibs)
        {
        }
    }
}