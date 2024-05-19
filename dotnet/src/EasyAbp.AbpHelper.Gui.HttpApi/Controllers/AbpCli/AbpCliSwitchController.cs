using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Switch;
using EasyAbp.AbpHelper.Gui.AbpCli.Switch.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.AbpHelper.Gui.Controllers.AbpCli
{
    [RemoteService]
    [Route("/api/abp-helper/abp-cli/switch")]
    public class AbpCliSwitchController : GuiController, IAbpCliSwitchAppService
    {
        private readonly IAbpCliSwitchAppService _service;

        public AbpCliSwitchController(IAbpCliSwitchAppService service)
        {
            _service = service;
        }
        
        [HttpPost]
        [Route("to-preview")]
        public Task<ServiceExecutionResult> SwitchToPreviewAsync(AbpSwitchToPreviewInput input)
        {
            return _service.SwitchToPreviewAsync(input);
        }

        [HttpPost]
        [Route("to-nightly")]
        public Task<ServiceExecutionResult> SwitchToNightlyAsync(AbpSwitchToNightlyInput input)
        {
            return _service.SwitchToNightlyAsync(input);
        }

        [HttpPost]
        [Route("to-stable")]
        public Task<ServiceExecutionResult> SwitchToStableAsync(AbpSwitchToStableInput input)
        {
            return _service.SwitchToStableAsync(input);
        }

        [HttpPost]
        [Route("to-prerc")]
        public Task<ServiceExecutionResult> SwitchToPreRcAsync(AbpSwitchToPreRcInput input)
        {
            return _service.SwitchToPreRcAsync(input);
        }

        [HttpPost]
        [Route("to-local")]
        public Task<ServiceExecutionResult> SwitchToLocalAsync(AbpSwitchToLocalInput input)
        {
            return _service.SwitchToLocalAsync(input);
        }
    }
}