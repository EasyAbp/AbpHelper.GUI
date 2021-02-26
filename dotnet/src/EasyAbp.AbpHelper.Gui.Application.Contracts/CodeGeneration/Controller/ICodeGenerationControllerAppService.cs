using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Controller.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.CodeGeneration.Controller
{
    public interface ICodeGenerationControllerAppService : IApplicationService
    {
        Task<ServiceExecutionResult> GenerateAsync(AbpHelperGenerateControllerInput input);
    }
}