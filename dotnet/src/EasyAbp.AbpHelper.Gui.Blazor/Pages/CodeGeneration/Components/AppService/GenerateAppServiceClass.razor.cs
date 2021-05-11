using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.CodeGeneration.AppService;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.CodeGeneration.Components.AppService
{
    public partial class GenerateAppServiceClass
    {
        [Inject]
        private ICodeGenerationAppServiceAppService Service { get; set; }

        protected override async Task InternalExecuteAsync()
        {
            await Service.GenerateClassAsync(Input);
        }
    }
}
