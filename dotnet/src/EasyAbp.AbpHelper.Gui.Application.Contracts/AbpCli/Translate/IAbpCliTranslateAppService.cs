using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Translate.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Translate
{
    public interface IAbpCliTranslateAppService : IApplicationService
    {
        Task<ServiceExecutionResult> CreateTranslationFileAsync(AbpCreateTranslationFileInput input);
        
        Task<ServiceExecutionResult> ApplyChangesAsync(AbpApplyChangesInput input);
    }
}