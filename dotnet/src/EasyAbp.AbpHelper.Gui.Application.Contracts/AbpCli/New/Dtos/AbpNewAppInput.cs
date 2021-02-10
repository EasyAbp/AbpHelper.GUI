using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.New.Dtos
{
    public class AbpNewAppInput : AbpNewInput
    {
        public override string Template { get; } = "app";
        
        public virtual AppUiFramework Ui { get; set; }
        
        /// <summary>
        /// Takes effect when the template is NOT mvc.
        /// </summary>
        public virtual bool SeparateIdentityServer { get; set; }
        
        /// <summary>
        /// Takes effect when the template is mvc.
        /// </summary>
        public virtual bool Tiered { get; set; }
        
        public virtual AppMobileApplicationFramework Mobile { get; set; }
        
        public virtual AppDatabaseProvider DatabaseProvider { get; set; }

        public AbpNewAppInput()
        {
            
        }
        
        public AbpNewAppInput([NotNull] string solutionName, [NotNull] string outputFolder, [CanBeNull] string version,
            bool preview, [CanBeNull] string templateSource, bool createSolutionFolder,
            [CanBeNull] string connectionString, Database databaseManagementSystem,
            [CanBeNull] string localFrameworkRef, bool noRandomPort, AppUiFramework ui,
            bool separateIdentityServer, bool tiered, AppMobileApplicationFramework mobile,
            AppDatabaseProvider databaseProvider) : base(solutionName, outputFolder, version, preview, templateSource,
            createSolutionFolder, connectionString, databaseManagementSystem, localFrameworkRef, noRandomPort)
        {
            Ui = ui;
            SeparateIdentityServer = separateIdentityServer;
            Tiered = tiered;
            Mobile = mobile;
            DatabaseProvider = databaseProvider;
        }
    }
}