using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.InstallLibs.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.AbpCli.InstallLibs
{
    public interface IAbpCliInstallLibsAppService : IApplicationService
    {
        Task<ServiceExecutionResult> InstallLibsAsync(AbpInstallLibsInput input);
    }
}