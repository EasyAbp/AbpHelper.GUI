using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Localization;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.CodeGeneration.Components.Localization
{
    public partial class GenerateLocalizationItems
    {
        [Inject]
        private ICodeGenerationLocalizationAppService Service { get; set; }

        protected override async Task InternalExecuteAsync()
        {
            await Service.GenerateItemsAsync(Input);
        }
    }
}
