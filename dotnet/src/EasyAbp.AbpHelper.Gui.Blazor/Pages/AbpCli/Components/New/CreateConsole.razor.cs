using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.New;
using EasyAbp.AbpHelper.Gui.AbpCli.New.Dtos;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Messages;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.AbpCli.Components.New
{
    public partial class CreateConsole
    {
        [Inject]
        private IAbpCliNewAppService Service { get; set; }
        
        protected AbpNewConsoleInput Input { get; set; } = new()
        {
            DatabaseManagementSystem = Database.SqlServer
        };

        protected override async Task ExecuteInternalAsync()
        {
            await Service.CreateConsoleAsync(Input);
        }
    }
}
