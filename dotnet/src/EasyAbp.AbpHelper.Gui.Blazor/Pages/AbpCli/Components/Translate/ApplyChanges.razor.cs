using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Translate;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Pages.AbpCli.Components.Translate
{
    public partial class ApplyChanges
    {
        [Inject]
        private IAbpCliTranslateAppService Service { get; set; }

        protected override async Task InternalExecuteAsync()
        {
            await Service.ApplyChangesAsync(Input);
        }
    }
}
