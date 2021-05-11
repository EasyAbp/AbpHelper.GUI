using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Update;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.AbpCli.Components.Update
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
