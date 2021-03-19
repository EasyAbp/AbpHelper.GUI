using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.New.Dtos
{
    public class AbpNewConsoleInput : AbpNewInput
    {
        public override string Template { get; } = "console";

        public AbpNewConsoleInput()
        {

        }

        public AbpNewConsoleInput([NotNull] string solutionName, [NotNull] string outputFolder,
            [CanBeNull] string version, bool preview, [CanBeNull] string templateSource, bool createSolutionFolder,
            [CanBeNull] string connectionString, Database databaseManagementSystem,
            [CanBeNull] string localFrameworkRef, bool noRandomPort, string template) : base(solutionName,
            outputFolder, version, preview, templateSource, createSolutionFolder, connectionString,
            databaseManagementSystem, localFrameworkRef, noRandomPort)
        {
            Template = template;
        }
    }
}