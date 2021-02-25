using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Login;
using EasyAbp.AbpHelper.Gui.AbpCli.Login.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.AbpHelper.Gui.Controllers.AbpCli
{
    [RemoteService]
    [Route("/api/abp-helper/abp-cli/login")]
    public class AbpCliLoginController : GuiController, IAbpCliLoginAppService
    {
        private readonly IAbpCliLoginAppService _service;

        public AbpCliLoginController(IAbpCliLoginAppService service)
        {
            _service = service;
        }
        
        [HttpPost]
        [Route("login")]
        public Task<ServiceExecutionResult> LoginAsync(AbpLoginInput input)
        {
            return _service.LoginAsync(input);
        }

        [HttpPost]
        [Route("logout")]
        public Task<ServiceExecutionResult> LogoutAsync()
        {
            return _service.LogoutAsync();
        }
    }
}