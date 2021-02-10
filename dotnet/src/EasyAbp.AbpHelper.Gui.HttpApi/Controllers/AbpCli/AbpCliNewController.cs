
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
    public class AbpCliNewController : GuiController, IAbpCliNewService
    {
        private readonly IAbpCliNewService _service;

        public AbpCliNewController(IAbpCliNewService service)
        {
            _service = service;
        }
        
        [HttpPost]
        [Route("app")]
        public virtual Task<ServiceExecutionResult> CreateAppAsync(AbpNewAppInput input)
        {
            return _service.CreateAppAsync(input);
        }
    }
}