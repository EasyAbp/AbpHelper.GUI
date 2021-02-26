using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.CodeGeneration.AppService.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.CodeGeneration.AppService
{
    public interface ICodeGenerationAppServiceAppService : IApplicationService
    {
        Task<ServiceExecutionResult> GenerateClassAsync(AbpHelperGenerateAppServiceClassInput input);
        
        Task<ServiceExecutionResult> GenerateMethodsAsync(AbpHelperGenerateAppServiceMethodsInput input);
    }
}