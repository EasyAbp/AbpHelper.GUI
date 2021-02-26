using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Migration.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.CodeGeneration.Migration
{
    public interface ICodeGenerationMigrationAppService : IApplicationService
    {
        Task<ServiceExecutionResult> AddAsync(AbpHelperGenerateMigrationAddInput input);
        
        Task<ServiceExecutionResult> RemoveAsync(AbpHelperGenerateMigrationRemoveInput input);
    }
}