using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.InstallLibs;
using EasyAbp.AbpHelper.Gui.AbpCli.InstallLibs.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.AbpHelper.Gui.Controllers.AbpCli
{
    [RemoteService]
    [Route("/api/abp-helper/abp-cli/build")]
    public class AbpCliInstallLibsController : GuiController, IAbpCliInstallLibsAppService
    {
        private readonly IAbpCliInstallLibsAppService _service;

        public AbpCliInstallLibsController(IAbpCliInstallLibsAppService service)
        {
            _service = service;
        }
        
        [HttpPost]
        public Task<ServiceExecutionResult> InstallLibsAsync(AbpInstallLibsInput input)
        {
            return _service.InstallLibsAsync(input);
        }
    }
}