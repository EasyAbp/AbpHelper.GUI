using System;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.LogService;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.LogService.Components
{
    public partial class Logs
    {
        [Inject]
        private ILogAppService Service { get; set; }
        
        [Inject]
        private IJSRuntime JsRuntime { get; set; }

        private async Task OpenRecentLogFileAsync()
        {
            var path = await GetRecentLogFilePathAsync();
            
            if (path.IsNullOrEmpty())
            {
                return;
            }
            
            await JsRuntime.InvokeVoidAsync("open", path, "_blank");
        }

        private async Task OpenRecentErrorLogFileAsync()
        {
            var path = await GetRecentErrorLogFilePathAsync();
            
            if (path.IsNullOrEmpty())
            {
                return;
            }
            
            await JsRuntime.InvokeVoidAsync("open", path, "_blank");
        }
        
        private async Task<string> GetRecentLogFilePathAsync()
        {
            return await Service.GetRecentLogFilePathAsync();
        }
        
        private async Task<string> GetRecentErrorLogFilePathAsync()
        {
            return await Service.GetRecentErrorLogFilePathAsync();
        }

    }
}