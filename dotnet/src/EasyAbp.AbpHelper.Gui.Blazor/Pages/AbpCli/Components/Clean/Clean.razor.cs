using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Clean;
using EasyAbp.AbpHelper.Gui.AbpCli.InstallLibs;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.AbpCli.Components.Clean
{
    public partial class Clean
    {
        [Inject]
        private IAbpCliCleanAppService Service { get; set; }

        protected override async Task InternalExecuteAsync()
        {
            await Service.CleanAsync(Input);
        }
    }
}
