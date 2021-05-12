using System.IO;
using System.Text;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.LogService;
using Elsa.Activities.Http.Activities;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.AbpHelper.Gui.Controllers;
using System;

namespace EasyAbp.AbpHelper.Gui.Blazor.Controllers
{
    [Route("/logs")]
    public class LogController : GuiController
    {
        private readonly ILogAppService _service;

        public LogController(ILogAppService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [Route("recent-error-log-file")]
        public async Task<string> GetRecentErrorLogAsync()
        {
            var path = await _service.GetRecentErrorLogFilePathAsync();

            if (!System.IO.File.Exists(path))
            {
                return string.Empty;
            }

            await using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            
            var buffer = new byte[1024 * 1024 * 10];
            
            var r = await stream.ReadAsync(buffer.AsMemory(0, buffer.Length));
            
            var str = Encoding.UTF8.GetString(buffer, 0, r);
            
            return str;
        }
    }
}