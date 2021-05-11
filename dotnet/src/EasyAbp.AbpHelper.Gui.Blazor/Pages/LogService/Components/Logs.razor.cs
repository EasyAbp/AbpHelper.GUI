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
            await JsRuntime.InvokeVoidAsync("open", await GetRecentLogFilePathAsync(), "_blank");
        }

        private async Task OpenRecentErrorLogFileAsync()
        {
            await JsRuntime.InvokeVoidAsync("open", await GetRecentErrorLogFilePathAsync(), "_blank");
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