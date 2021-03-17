using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Solutions;
using EasyAbp.AbpHelper.Gui.Solutions.Dtos;
using EasyAbp.AbpHelper.Gui.UpdateCheck;
using EasyAbp.AbpHelper.Gui.UpdateCheck.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.AbpHelper.Gui.Controllers.UpdateCheck
{
    [RemoteService]
    [Route("/api/abp-helper/update-check")]
    public class UpdateCheckController : GuiController, IUpdateCheckAppService
    {
        private readonly IUpdateCheckAppService _service;

        public UpdateCheckController(IUpdateCheckAppService service)
        {
            _service = service;
        }

        [HttpPost]
        public Task<UpdateCheckOutput> CheckAsync()
        {
            return _service.CheckAsync();
        }
    }
}