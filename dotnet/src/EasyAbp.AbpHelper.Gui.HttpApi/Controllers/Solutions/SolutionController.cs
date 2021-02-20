using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Solutions;
using EasyAbp.AbpHelper.Gui.Solutions.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.AbpHelper.Gui.Controllers.Solutions
{
    [RemoteService]
    [Route("/api/abp-helper/solutions")]
    public class SolutionController : GuiController, ISolutionAppService
    {
        private readonly ISolutionAppService _service;

        public SolutionController(ISolutionAppService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public virtual Task<ListResultDto<SolutionDto>> GetListAsync()
        {
            return _service.GetListAsync();
        }

        [HttpPost]
        [Route("use")]
        public virtual Task<SolutionDto> UseAsync(SolutionDto input)
        {
            return _service.UseAsync(input);
        }

        [HttpDelete]
        public virtual Task DeleteAsync(SolutionDto input)
        {
            return _service.DeleteAsync(input);
        }
    }
}