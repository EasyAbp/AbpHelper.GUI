using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.InstallLibs;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.AbpCli.Components.InstallLibs
{
    public partial class InstallLibs
    {
        [Inject]
        private IAbpCliInstallLibsAppService Service { get; set; }

        protected override async Task InternalExecuteAsync()
        {
            await Service.InstallLibsAsync(Input);
        }
    }
}
