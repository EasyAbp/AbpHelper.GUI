using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.GetSource;
using EasyAbp.AbpHelper.Gui.AbpCli.GetSource.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.AbpHelper.Gui.Controllers.AbpCli
{
    [RemoteService]
    [Route("/api/abp-helper/abp-cli/get-source")]
    public class AbpCliGetSourceController : GuiController, IAbpCliGetSourceAppService
    {
        private readonly IAbpCliGetSourceAppService _service;

        public AbpCliGetSourceController(IAbpCliGetSourceAppService service)
        {
            _service = service;
        }
        
        [HttpPost]
        public virtual Task<ServiceExecutionResult> GetSourceAsync(AbpGetSourceInput input)
        {
            return _service.GetSourceAsync(input);
        }
    }
}