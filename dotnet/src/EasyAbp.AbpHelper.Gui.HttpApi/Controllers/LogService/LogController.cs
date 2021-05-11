using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.LogService;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.AbpHelper.Gui.Controllers.LogService
{
    [RemoteService]
    [Route("/api/abp-helper/logs")]
    public class LogController : GuiController, ILogAppService
    {
        private readonly ILogAppService _service;

        public LogController(ILogAppService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [Route("recent-log-file-path")]
        public Task<string> GetRecentLogFilePathAsync()
        {
            return _service.GetRecentLogFilePathAsync();
        }

        [HttpGet]
        [Route("recent-error-log-file-path")]
        public Task<string> GetRecentErrorLogFilePathAsync()
        {
            return _service.GetRecentErrorLogFilePathAsync();
        }
    }
}