using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Bundle;
using EasyAbp.AbpHelper.Gui.AbpCli.Bundle.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.AbpHelper.Gui.Controllers.AbpCli
{
    [RemoteService]
    [Route("/api/abp-helper/abp-cli/bundle")]
    public class AbpCliBundleController : GuiController, IAbpCliBundleAppService
    {
        private readonly IAbpCliBundleAppService _service;

        public AbpCliBundleController(IAbpCliBundleAppService service)
        {
            _service = service;
        }
        
        [HttpPost]
        public Task<ServiceExecutionResult> RunAsync(AbpBundleInput input)
        {
            return _service.RunAsync(input);
        }
    }
}