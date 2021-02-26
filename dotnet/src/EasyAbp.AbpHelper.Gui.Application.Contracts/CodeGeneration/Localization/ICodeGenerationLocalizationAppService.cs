using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Localization.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.CodeGeneration.Localization
{
    public interface ICodeGenerationLocalizationAppService : IApplicationService
    {
        Task<ServiceExecutionResult> GenerateItemsAsync(AbpHelperGenerateLocalizationItemsInput input);
    }
}