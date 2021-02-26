using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Crud;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Crud.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.AbpHelper.Gui.Controllers.CodeGeneration
{
    [RemoteService]
    [Route("/api/abp-helper/code-generation/crud")]
    public class CodeGenerationCrudController : GuiController, ICodeGenerationCrudAppService
    {
        private readonly ICodeGenerationCrudAppService _service;

        public CodeGenerationCrudController(ICodeGenerationCrudAppService service)
        {
            _service = service;
        }
        
        [HttpPost]
        public Task<ServiceExecutionResult> GenerateAsync(AbpHelperGenerateCrudInput input)
        {
            return _service.GenerateAsync(input);
        }
    }
}