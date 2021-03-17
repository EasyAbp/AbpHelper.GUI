using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.UpdateCheck;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Pages
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
    }
}
