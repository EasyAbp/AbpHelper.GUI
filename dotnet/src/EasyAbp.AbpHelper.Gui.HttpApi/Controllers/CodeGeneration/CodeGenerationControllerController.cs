using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Controller;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Controller.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.AbpHelper.Gui.Controllers.CodeGeneration
{
    [RemoteService]
    [Route("/api/abp-helper/code-generation/controller")]
    public class CodeGenerationControllerController : GuiController, ICodeGenerationControllerAppService
    {
        private readonly ICodeGenerationControllerAppService _service;

        public CodeGenerationControllerController(ICodeGenerationControllerAppService service)
        {
            _service = service;
        }
        
        [HttpPost]
        public Task<ServiceExecutionResult> GenerateAsync(AbpHelperGenerateControllerInput input)
        {
            return _service.GenerateAsync(input);
        }
    }
}