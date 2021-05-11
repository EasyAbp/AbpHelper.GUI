using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Build;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.AbpCli.Components.Build
{
    public partial class Build
    {
        [Inject]
        private IAbpCliBuildAppService Service { get; set; }

        protected override async Task InternalExecuteAsync()
        {
            await Service.BuildAsync(Input);
        }
    }
}
