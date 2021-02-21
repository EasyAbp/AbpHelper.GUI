using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Update;
using EasyAbp.AbpHelper.Gui.AbpCli.Update.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.AbpHelper.Gui.Controllers.AbpCli
{
    [RemoteService]
    [Route("/api/abp-helper/abp-cli/update")]
    public class AbpCliUpdateController : GuiController, IAbpCliUpdateAppService
    {
        private readonly IAbpCliUpdateAppService _service;

        public AbpCliUpdateController(IAbpCliUpdateAppService service)
        {
            _service = service;
        }
        
        [HttpPost]
        [Route("update")]
        public virtual Task<ServiceExecutionResult> UpdateAsync(AbpUpdateInput input)
        {
            return _service.UpdateAsync(input);
        }
    }
}