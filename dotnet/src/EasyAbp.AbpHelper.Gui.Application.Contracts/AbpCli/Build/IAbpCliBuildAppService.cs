using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Build.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Build
{
    public interface IAbpCliBuildAppService : IApplicationService
    {
        Task<ServiceExecutionResult> BuildAsync(AbpBuildInput input);
    }
}