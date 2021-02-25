using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Proxy.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Proxy
{
    public interface IAbpCliProxyAppService : IApplicationService
    {
        Task<ServiceExecutionResult> GenerateProxyAsync(AbpGenerateProxyInput input);
        
        Task<ServiceExecutionResult> RemoveProxyAsync(AbpRemoveProxyInput input);
    }
}