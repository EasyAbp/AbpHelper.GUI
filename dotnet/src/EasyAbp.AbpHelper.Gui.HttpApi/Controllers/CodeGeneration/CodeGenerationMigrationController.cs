using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Migration;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Migration.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.AbpHelper.Gui.Controllers.CodeGeneration
{
    [RemoteService]
    [Route("/api/abp-helper/code-generation/migration")]
    public class CodeGenerationMigrationController : GuiController, ICodeGenerationMigrationAppService
    {
        private readonly ICodeGenerationMigrationAppService _service;

        public CodeGenerationMigrationController(ICodeGenerationMigrationAppService service)
        {
            _service = service;
        }
        
        [HttpPost]
        [Route("add")]
        public Task<ServiceExecutionResult> AddAsync(AbpHelperGenerateMigrationAddInput input)
        {
            return _service.AddAsync(input);
        }

        [HttpPost]
        [Route("remove")]
        public Task<ServiceExecutionResult> RemoveAsync(AbpHelperGenerateMigrationRemoveInput input)
        {
            return _service.RemoveAsync(input);
        }
    }
}