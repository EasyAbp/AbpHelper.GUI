using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Build;
using EasyAbp.AbpHelper.Gui.AbpCli.Build.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.AbpHelper.Gui.Controllers.AbpCli
{
    [RemoteService]
    [Route("/api/abp-helper/abp-cli/build")]
    public class AbpCliBuildController : GuiController, IAbpCliBuildAppService
    {
        private readonly IAbpCliBuildAppService _service;

        public AbpCliBuildController(IAbpCliBuildAppService service)
        {
            _service = service;
        }
        
        [HttpPost]
        public Task<ServiceExecutionResult> BuildAsync(AbpBuildInput input)
        {
            return _service.BuildAsync(input);
        }
    }
}