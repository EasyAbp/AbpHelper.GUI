using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.New.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.AbpCli.New
{
    public interface IAbpCliNewAppService : IApplicationService
    {
        Task<ServiceExecutionResult> CreateAppAsync(AbpNewAppInput input);
        
        Task<ServiceExecutionResult> CreateAppNoLayersAsync(AbpNewAppNoLayersInput input);
        
        Task<ServiceExecutionResult> CreateModuleAsync(AbpNewModuleInput input);

        Task<ServiceExecutionResult> CreateConsoleAsync(AbpNewConsoleInput input);
    }
}