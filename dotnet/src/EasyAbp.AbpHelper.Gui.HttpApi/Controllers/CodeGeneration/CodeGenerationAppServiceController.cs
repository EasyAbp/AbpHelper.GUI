using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.CodeGeneration.AppService;
using EasyAbp.AbpHelper.Gui.CodeGeneration.AppService.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.AbpHelper.Gui.Controllers.CodeGeneration
{
    [RemoteService]
    [Route("/api/abp-helper/code-generation/app-service")]
    public class CodeGenerationAppServiceController : GuiController, ICodeGenerationAppServiceAppService
    {
        private readonly ICodeGenerationAppServiceAppService _service;

        public CodeGenerationAppServiceController(ICodeGenerationAppServiceAppService service)
        {
            _service = service;
        }
        
        [HttpPost]
        [Route("class")]
        public Task<ServiceExecutionResult> GenerateClassAsync(AbpHelperGenerateAppServiceClassInput input)
        {
            return _service.GenerateClassAsync(input);
        }

        [HttpPost]
        [Route("methods")]
        public Task<ServiceExecutionResult> GenerateMethodsAsync(AbpHelperGenerateAppServiceMethodsInput input)
        {
            return _service.GenerateMethodsAsync(input);
        }
    }
}