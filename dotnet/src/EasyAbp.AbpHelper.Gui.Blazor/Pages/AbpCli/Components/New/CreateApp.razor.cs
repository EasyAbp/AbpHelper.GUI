using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.New;
using EasyAbp.AbpHelper.Gui.AbpCli.New.Dtos;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.AbpCli.Components.New
{
    public partial class CreateApp
    {
        [Inject]
        private IAbpCliNewAppService Service { get; set; }

        protected AbpNewAppInput Input { get; set; } = new()
        {
            Ui = AppUiFramework.Mvc,
            Mobile = AppMobileApplicationFramework.None,
            DatabaseProvider = AppDatabaseProvider.Ef,
            DatabaseManagementSystem = Database.SqlServer,
            Version = AbpVersionHelper.AbpVersion
        };

        public bool HasTieredOption => Input.Ui is AppUiFramework.Mvc or AppUiFramework.BlazorServer;

        public bool HasSeparateAuthServerOption =>
            Input.Ui is AppUiFramework.Angular or AppUiFramework.Blazor or AppUiFramework.None;

        public bool HasPwaOption => Input.Ui is AppUiFramework.Angular or AppUiFramework.Blazor;

        protected override async Task InternalExecuteAsync()
        {
            await Service.CreateAppAsync(Input);
        }
    }
}