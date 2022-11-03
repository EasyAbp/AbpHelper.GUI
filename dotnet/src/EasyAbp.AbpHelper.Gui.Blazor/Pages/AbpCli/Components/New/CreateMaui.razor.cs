using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.New;
using EasyAbp.AbpHelper.Gui.AbpCli.New.Dtos;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.AbpCli.Components.New
{
    public partial class CreateMaui
    {
        [Inject]
        private IAbpCliNewAppService Service { get; set; }

        protected AbpNewMauiInput Input { get; set; } = new()
        {
            DatabaseManagementSystem = Database.SqlServer
        };

        protected override async Task InternalExecuteAsync()
        {
            await Service.CreateMauiAsync(Input);
        }
    }
}