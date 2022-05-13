using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Proxy.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Proxy
{
    public interface IAbpCliProxyAppService : IApplicationService
    {
        Task<ServiceExecutionResult> GenerateAngularProxyAsync(AbpGenerateRemoveAngularProxyInput input);
        
        Task<ServiceExecutionResult> RemoveAngularProxyAsync(AbpGenerateRemoveAngularProxyInput input);
        
        Task<ServiceExecutionResult> GenerateCSharpProxyAsync(AbpGenerateRemoveCSharpProxyInput input);
        
        Task<ServiceExecutionResult> RemoveCSharpProxyAsync(AbpGenerateRemoveCSharpProxyInput input);
        
        Task<ServiceExecutionResult> GenerateJavaScriptProxyAsync(AbpGenerateRemoveJavaScriptProxyInput input);
        
        Task<ServiceExecutionResult> RemoveJavaScriptProxyAsync(AbpGenerateRemoveJavaScriptProxyInput input);
    }
}