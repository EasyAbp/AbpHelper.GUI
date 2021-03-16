using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Bundle;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Pages.AbpCli.Components.Bundle
{
    public partial class Bundle
    {
        [Inject]
        private IAbpCliBundleAppService Service { get; set; }

        protected override async Task InternalExecuteAsync()
        {
            await Service.RunAsync(Input);
        }
    }
}
