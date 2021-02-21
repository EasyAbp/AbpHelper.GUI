using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Add;
using EasyAbp.AbpHelper.Gui.AbpCli.Add.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.AbpHelper.Gui.Controllers.AbpCli
{
    [RemoteService]
    [Route("/api/abp-helper/abp-cli/add")]
    public class AbpCliAddController : GuiController, IAbpCliAddAppService
    {
        private readonly IAbpCliAddAppService _service;

        public AbpCliAddController(IAbpCliAddAppService service)
        {
            _service = service;
        }
        
        [HttpPost]
        [Route("package")]
        public Task<ServiceExecutionResult> AddPackageAsync(AbpAddPackageInput input)
        {
            return _service.AddPackageAsync(input);

        }

        [HttpPost]
        [Route("module")]
        public Task<ServiceExecutionResult> AddModuleAsync(AbpAddModuleInput input)
        {
            return _service.AddModuleAsync(input);
        }
    }
}