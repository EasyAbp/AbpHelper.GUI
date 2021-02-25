using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.GetSource;
using EasyAbp.AbpHelper.Gui.AbpCli.Update;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.AbpCli.Components.GetSource
{
    public partial class GetSource
    {
        [Inject]
        private IAbpCliGetSourceAppService Service { get; set; }

        protected override async Task InternalExecuteAsync()
        {
            await Service.GetSourceAsync(Input);
        }
    }
}
