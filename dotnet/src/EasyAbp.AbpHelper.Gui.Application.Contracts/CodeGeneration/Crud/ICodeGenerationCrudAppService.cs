using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Crud.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.CodeGeneration.Crud
{
    public interface ICodeGenerationCrudAppService : IApplicationService
    {
        Task<ServiceExecutionResult> GenerateAsync(AbpHelperGenerateCrudInput input);
    }
}