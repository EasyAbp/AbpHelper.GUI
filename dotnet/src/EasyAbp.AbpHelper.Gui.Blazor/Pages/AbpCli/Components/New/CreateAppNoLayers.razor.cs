using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.New;
using EasyAbp.AbpHelper.Gui.AbpCli.New.Dtos;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.AbpCli.Components.New
{
    public partial class CreateAppNoLayers
    {
        [Inject]
        private IAbpCliNewAppService Service { get; set; }
        
        protected AbpNewAppNoLayersInput Input { get; set; } = new()
        {
            Ui = AppUiFramework.Mvc,
            DatabaseProvider = AppDatabaseProvider.Ef,
            DatabaseManagementSystem = Database.SqlServer
        };

        protected override async Task InternalExecuteAsync()
        {
            await Service.CreateAppNoLayersAsync(Input);
        }
    }
}
