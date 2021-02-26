using System.Threading.Tasks;
using EasyAbp.AbpHelper.Core.Commands.Generate.Crud;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Crud.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;

namespace EasyAbp.AbpHelper.Gui.CodeGeneration.Crud
{
    public class CodeGenerationCrudAppService : CodeGenerationAppService, ICodeGenerationCrudAppService
    {
        private readonly CrudCommand _crudCommand;

        public CodeGenerationCrudAppService(CrudCommand crudCommand)
        {
            _crudCommand = crudCommand;
        }
        
        public virtual async Task<ServiceExecutionResult> GenerateAsync(AbpHelperGenerateCrudInput input)
        {
            await _crudCommand.RunCommand(ObjectMapper.Map<AbpHelperGenerateCrudInput, CrudCommandOption>(input));

            return new ServiceExecutionResult(true);
        }
    }
}