using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Migration;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.CodeGeneration.Components.Migration
{
    public partial class RemoveEfMigration
    {
        [Inject]
        private ICodeGenerationMigrationAppService Service { get; set; }

        protected override async Task InternalExecuteAsync()
        {
            await Service.RemoveAsync(Input);
        }
    }
}
