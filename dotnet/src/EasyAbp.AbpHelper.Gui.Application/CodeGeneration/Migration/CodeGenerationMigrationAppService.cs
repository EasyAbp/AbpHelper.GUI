using System.Threading.Tasks;
using EasyAbp.AbpHelper.Core.Commands.Ef.Migrations.Add;
using EasyAbp.AbpHelper.Core.Commands.Ef.Migrations.Remove;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Migration;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Migration.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;

namespace EasyAbp.AbpHelper.Gui.CodeGeneration.Migration
{
    public class CodeGenerationMigrationAppService : CodeGenerationAppService, ICodeGenerationMigrationAppService
    {
        private readonly AddCommand _addCommand;
        private readonly RemoveCommand _removeCommand;

        public CodeGenerationMigrationAppService(
            AddCommand addCommand,
            RemoveCommand removeCommand)
        {
            _addCommand = addCommand;
            _removeCommand = removeCommand;
        }

        public virtual async Task<ServiceExecutionResult> AddAsync(AbpHelperGenerateMigrationAddInput input)
        {
            await _addCommand.RunCommand(ObjectMapper.Map<AbpHelperGenerateMigrationAddInput, AddCommandOption>(input));

            return new ServiceExecutionResult(true);
        }

        public virtual async Task<ServiceExecutionResult> RemoveAsync(AbpHelperGenerateMigrationRemoveInput input)
        {
            await _removeCommand.RunCommand(ObjectMapper.Map<AbpHelperGenerateMigrationRemoveInput, RemoveCommandOption>(input));

            return new ServiceExecutionResult(true);
        }
    }
}