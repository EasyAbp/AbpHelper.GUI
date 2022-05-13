using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Proxy;
using EasyAbp.AbpHelper.Gui.AbpCli.Proxy.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.AbpHelper.Gui.Controllers.AbpCli
{
    [RemoteService]
    [Route("/api/abp-helper/abp-cli/proxy")]
    public class AbpCliProxyController : GuiController, IAbpCliProxyAppService
    {
        private readonly IAbpCliProxyAppService _service;

        public AbpCliProxyController(IAbpCliProxyAppService service)
        {
            _service = service;
        }
        
        [HttpPost]
        [Route("generate/ng")]
        public Task<ServiceExecutionResult> GenerateAngularProxyAsync(AbpGenerateRemoveAngularProxyInput input)
        {
            return _service.GenerateAngularProxyAsync(input);
        }

        [HttpPost]
        [Route("remove/ng")]
        public virtual Task<ServiceExecutionResult> RemoveAngularProxyAsync(AbpGenerateRemoveAngularProxyInput input)
        {
            return _service.RemoveAngularProxyAsync(input);
        }

        [HttpPost]
        [Route("generate/csharp")]
        public virtual Task<ServiceExecutionResult> GenerateCSharpProxyAsync(AbpGenerateRemoveCSharpProxyInput input)
        {
            return _service.GenerateCSharpProxyAsync(input);
        }

        [HttpPost]
        [Route("remove/csharp")]
        public virtual Task<ServiceExecutionResult> RemoveCSharpProxyAsync(AbpGenerateRemoveCSharpProxyInput input)
        {
            return _service.RemoveCSharpProxyAsync(input);
        }

        [HttpPost]
        [Route("generate/js")]
        public virtual Task<ServiceExecutionResult> GenerateJavaScriptProxyAsync(AbpGenerateRemoveJavaScriptProxyInput input)
        {
            return _service.GenerateJavaScriptProxyAsync(input);
        }

        [HttpPost]
        [Route("remove/js")]
        public virtual Task<ServiceExecutionResult> RemoveJavaScriptProxyAsync(AbpGenerateRemoveJavaScriptProxyInput input)
        {
            return _service.RemoveJavaScriptProxyAsync(input);
        }
    }
}