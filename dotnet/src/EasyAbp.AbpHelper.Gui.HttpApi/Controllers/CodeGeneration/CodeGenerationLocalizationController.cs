using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Localization;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Localization.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.AbpHelper.Gui.Controllers.CodeGeneration
{
    [RemoteService]
    [Route("/api/abp-helper/code-generation/localization")]
    public class CodeGenerationLocalizationController : GuiController, ICodeGenerationLocalizationAppService
    {
        private readonly ICodeGenerationLocalizationAppService _service;

        public CodeGenerationLocalizationController(ICodeGenerationLocalizationAppService service)
        {
            _service = service;
        }
        
        [HttpPost]
        [Route("items")]
        public Task<ServiceExecutionResult> GenerateItemsAsync(AbpHelperGenerateLocalizationItemsInput input)
        {
            return _service.GenerateItemsAsync(input);
        }
    }
}