using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.UpdateCheck;
using Microsoft.AspNetCore.Components;
using Volo.Abp.Cli;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages
{
    public partial class Index
    {
        [Inject]
        private IUpdateCheckAppService UpdateCheckAppService { get; set; }

        private bool UpdateCheckAlertVisible { get; set; }
        
        private string LatestVersion { get; set; }
        
        private string CurrentVersion { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var result = await UpdateCheckAppService.CheckAsync();

            LatestVersion = result.LatestVersion;
            CurrentVersion = result.CurrentVersion;
            UpdateCheckAlertVisible = result.ShouldUpdate;
        }

        protected virtual string GetAbpVersion()
        {
            return typeof(AbpCliCoreModule).Assembly.GetName().Version?.ToString(3);
        }

        protected virtual string GetAbpHelperGuiVersion()
        {
            return typeof(GuiBlazorModule).Assembly.GetName().Version?.ToString(3);
        }
    }
}
