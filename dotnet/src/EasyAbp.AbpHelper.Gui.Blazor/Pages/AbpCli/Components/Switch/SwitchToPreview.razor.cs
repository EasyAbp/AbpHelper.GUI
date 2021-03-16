using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Proxy;
using EasyAbp.AbpHelper.Gui.AbpCli.Switch;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Pages.AbpCli.Components.Switch
{
    public partial class SwitchToPreview
    {
        [Inject]
        private IAbpCliSwitchAppService Service { get; set; }

        protected override async Task InternalExecuteAsync()
        {
            await Service.SwitchToPreviewAsync(Input);
        }
    }
}
