using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Translate;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Pages.AbpCli.Components.Translate
{
    public partial class CreateTranslationFile
    {
        [Inject]
        private IAbpCliTranslateAppService Service { get; set; }

        protected override async Task InternalExecuteAsync()
        {
            await Service.CreateTranslationFileAsync(Input);
        }
    }
}
