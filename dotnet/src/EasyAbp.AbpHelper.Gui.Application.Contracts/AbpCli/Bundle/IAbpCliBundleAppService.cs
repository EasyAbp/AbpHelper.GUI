using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Bundle.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Bundle
{
    public interface IAbpCliBundleAppService : IApplicationService
    {
        Task<ServiceExecutionResult> RunAsync(AbpBundleInput input);
    }
}