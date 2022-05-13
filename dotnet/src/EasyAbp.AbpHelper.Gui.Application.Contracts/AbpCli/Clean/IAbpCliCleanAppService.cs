using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Clean.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Clean
{
    public interface IAbpCliCleanAppService : IApplicationService
    {
        Task<ServiceExecutionResult> CleanAsync(AbpCleanInput input);
    }
}