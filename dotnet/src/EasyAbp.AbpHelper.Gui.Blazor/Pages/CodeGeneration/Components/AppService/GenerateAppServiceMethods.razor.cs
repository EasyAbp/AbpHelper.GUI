using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.CodeGeneration.AppService;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Crud;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Pages.CodeGeneration.Components.AppService
{
    public partial class GenerateAppServiceMethods
    {
        [Inject]
        private ICodeGenerationAppServiceAppService Service { get; set; }

        protected override async Task InternalExecuteAsync()
        {
            await Service.GenerateMethodsAsync(Input);
        }
    }
}
