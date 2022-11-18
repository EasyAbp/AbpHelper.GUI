using System;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.New.Dtos
{
    [Serializable]
    public class AbpNewAppInput : AbpNewInputBase
    {
        public override string Template => "app";

        public virtual AppUiFramework Ui { get; set; }

        /// <summary>
        /// Takes effect when the template is `Angular`, `Blazor`, or `None`.
        /// </summary>
        public virtual bool SeparateAuthServer { get; set; }

        /// <summary>
        /// Takes effect when the template is `Mvc` or `BlazorServer`.
        /// </summary>
        public virtual bool Tiered { get; set; }

        /// <summary>
        /// Takes effect when the template is `Angular` or `Blazor`.
        /// </summary>
        public virtual bool Pwa { get; set; }

        public virtual AppMobileApplicationFramework Mobile { get; set; }

        public virtual AppDatabaseProvider DatabaseProvider { get; set; }

        public virtual AbpThemes Theme { get; set; }

        public virtual bool SkipBundling { get; set; }

        public AbpNewAppInput()
        {
        }

        public AbpNewAppInput([NotNull] string solutionName, [NotNull] string outputFolder, [CanBeNull] string version,
            bool preview, [CanBeNull] string templateSource, bool createSolutionFolder,
            [CanBeNull] string connectionString, Database databaseManagementSystem,
            [CanBeNull] string localFrameworkRef, bool noRandomPort, bool skipInstallingLibs, AppUiFramework ui,
            bool separateAuthServer, bool tiered, bool pwa, AppMobileApplicationFramework mobile,
            AppDatabaseProvider databaseProvider, AbpThemes theme, bool skipBundling) : base(solutionName, outputFolder,
            version, preview, templateSource, createSolutionFolder, connectionString, databaseManagementSystem,
            localFrameworkRef, noRandomPort, skipInstallingLibs)
        {
            Ui = ui;
            SeparateAuthServer = separateAuthServer;
            Tiered = tiered;
            Pwa = pwa;
            Mobile = mobile;
            DatabaseProvider = databaseProvider;
            Theme = theme;
            SkipBundling = skipBundling;
        }
    }
}