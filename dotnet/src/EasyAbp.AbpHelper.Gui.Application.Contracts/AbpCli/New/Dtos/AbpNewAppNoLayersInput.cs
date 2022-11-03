using System;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.New.Dtos
{
    [Serializable]
    public class AbpNewAppNoLayersInput : AbpNewInputBase
    {
        public override string Template => "app-nolayers";

        public virtual AppUiFramework Ui { get; set; }

        public virtual AppDatabaseProvider DatabaseProvider { get; set; }

        public AbpNewAppNoLayersInput()
        {
        }

        public AbpNewAppNoLayersInput([NotNull] string solutionName, [NotNull] string outputFolder,
            [CanBeNull] string version, bool preview, [CanBeNull] string templateSource, bool createSolutionFolder,
            [CanBeNull] string connectionString, Database databaseManagementSystem,
            [CanBeNull] string localFrameworkRef, bool noRandomPort, bool skipInstallingLibs, AppUiFramework ui,
            AppDatabaseProvider databaseProvider) : base(solutionName, outputFolder, version,
            preview, templateSource, createSolutionFolder, connectionString, databaseManagementSystem,
            localFrameworkRef, noRandomPort, skipInstallingLibs)
        {
            Ui = ui;
            DatabaseProvider = databaseProvider;
        }
    }
}