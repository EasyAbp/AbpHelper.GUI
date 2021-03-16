using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Controller;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Pages.CodeGeneration.Components.Controller
{
    public partial class GenerateController
    {
        [Inject]
        private ICodeGenerationControllerAppService Service { get; set; }

        protected override async Task InternalExecuteAsync()
        {
            await Service.GenerateAsync(Input);
        }
    }
}
