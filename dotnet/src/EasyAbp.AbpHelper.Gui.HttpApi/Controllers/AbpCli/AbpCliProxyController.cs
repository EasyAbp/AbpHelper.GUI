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
        [Route("generate")]
        public Task<ServiceExecutionResult> GenerateProxyAsync(AbpGenerateProxyInput input)
        {
            return _service.GenerateProxyAsync(input);
        }

        [HttpPost]
        [Route("remove")]
        public Task<ServiceExecutionResult> RemoveProxyAsync(AbpRemoveProxyInput input)
        {
            return _service.RemoveProxyAsync(input);
        }
    }
}