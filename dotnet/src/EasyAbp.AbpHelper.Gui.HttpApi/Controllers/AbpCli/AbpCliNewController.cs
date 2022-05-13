using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.New;
using EasyAbp.AbpHelper.Gui.AbpCli.New.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.AbpHelper.Gui.Controllers.AbpCli
{
    [RemoteService]
    [Route("/api/abp-helper/abp-cli/new")]
    public class AbpCliNewController : GuiController, IAbpCliNewAppService
    {
        private readonly IAbpCliNewAppService _service;

        public AbpCliNewController(IAbpCliNewAppService service)
        {
            _service = service;
        }
        
        [HttpPost]
        [Route("app")]
        public virtual Task<ServiceExecutionResult> CreateAppAsync(AbpNewAppInput input)
        {
            return _service.CreateAppAsync(input);
        }

        [HttpPost]
        [Route("app-nolayers")]
        public virtual Task<ServiceExecutionResult> CreateAppNoLayersAsync(AbpNewAppNoLayersInput input)
        {
            return _service.CreateAppNoLayersAsync(input);
        }

        [HttpPost]
        [Route("module")]
        public virtual Task<ServiceExecutionResult> CreateModuleAsync(AbpNewModuleInput input)
        {
            return _service.CreateModuleAsync(input);
        }

        [HttpPost]
        [Route("console")]
        public virtual Task<ServiceExecutionResult> CreateConsoleAsync(AbpNewConsoleInput input)
        {
            return _service.CreateConsoleAsync(input);
        }
    }
}