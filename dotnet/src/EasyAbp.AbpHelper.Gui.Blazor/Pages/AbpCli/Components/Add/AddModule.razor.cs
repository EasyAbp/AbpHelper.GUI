using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Add;
using EasyAbp.AbpHelper.Gui.AbpCli.Add.Dtos;
using EasyAbp.AbpHelper.Gui.AbpCli.Update;
using EasyAbp.AbpHelper.Gui.AbpCli.Update.Dtos;
using EasyAbp.AbpHelper.Gui.Services;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Pages.AbpCli.Components.Add
{
    public partial class AddModule
    {
        [Inject]
        private IAbpCliAddAppService Service { get; set; }
        
        protected override async Task InternalExecuteAsync()
        {
            await Service.AddModuleAsync(Input);
        }
    }
}
