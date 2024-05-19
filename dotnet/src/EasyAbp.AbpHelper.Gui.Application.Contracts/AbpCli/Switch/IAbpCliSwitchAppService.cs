using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Switch.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Switch
{
    public interface IAbpCliSwitchAppService : IApplicationService
    {
        Task<ServiceExecutionResult> SwitchToPreviewAsync(AbpSwitchToPreviewInput input);

        Task<ServiceExecutionResult> SwitchToNightlyAsync(AbpSwitchToNightlyInput input);

        Task<ServiceExecutionResult> SwitchToStableAsync(AbpSwitchToStableInput input);

        Task<ServiceExecutionResult> SwitchToPreRcAsync(AbpSwitchToPreRcInput input);

        Task<ServiceExecutionResult> SwitchToLocalAsync(AbpSwitchToLocalInput input);
    }
}