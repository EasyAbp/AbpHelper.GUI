using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.New;
using EasyAbp.AbpHelper.Gui.AbpCli.New.Dtos;
using EasyAbp.AbpHelper.Gui.AbpCli.Update;
using EasyAbp.AbpHelper.Gui.AbpCli.Update.Dtos;
using EasyAbp.AbpHelper.Gui.Services;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Pages.AbpCli.Components.Update
{
    public partial class Update
    {
        [Inject]
        private IAbpCliUpdateAppService Service { get; set; }

        protected override async Task InternalExecuteAsync()
        {
            await Service.UpdateAsync(Input);
        }
    }
}
