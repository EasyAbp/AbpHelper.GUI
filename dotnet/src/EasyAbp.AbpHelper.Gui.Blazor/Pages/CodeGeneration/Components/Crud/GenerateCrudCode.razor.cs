using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Translate;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Crud;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.CodeGeneration.Components.Crud
{
    public partial class GenerateCrudCode
    {
        [Inject]
        private ICodeGenerationCrudAppService Service { get; set; }

        protected override async Task InternalExecuteAsync()
        {
            await Service.GenerateAsync(Input);
        }
    }
}
