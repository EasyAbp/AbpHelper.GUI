using System;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.New.Dtos
{
    [Serializable]
    public class AbpNewAppNoLayersInput : AbpNewInput
    {
        public override string Template { get; } = "app-nolayers";
        
        public virtual AppUiFramework Ui { get; set; }
        
        /// <summary>
        /// Takes effect when the template is NOT mvc.
        /// </summary>
        public virtual bool SeparateIdentityServer { get; set; }
        
        /// <summary>
        /// Takes effect when the template is mvc.
        /// </summary>
        public virtual bool Tiered { get; set; }
        
        public virtual AppDatabaseProvider DatabaseProvider { get; set; }

        public AbpNewAppNoLayersInput()
        {
            
        }

        public AbpNewAppNoLayersInput([NotNull] string solutionName, [NotNull] string outputFolder,
            [CanBeNull] string version,
            bool preview, [CanBeNull] string templateSource, bool createSolutionFolder,
            [CanBeNull] string connectionString, Database databaseManagementSystem,
            [CanBeNull] string localFrameworkRef, bool noRandomPort, AppUiFramework ui,
            bool separateIdentityServer, bool tiered, AppDatabaseProvider databaseProvider) : base(solutionName,
            outputFolder, version, preview, templateSource, createSolutionFolder, connectionString,
            databaseManagementSystem, localFrameworkRef, noRandomPort)
        {
            Ui = ui;
            SeparateIdentityServer = separateIdentityServer;
            Tiered = tiered;
            DatabaseProvider = databaseProvider;
        }
    }
}