using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Login;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.AbpCli.Components.Login
{
    public partial class Logout
    {
        [Inject]
        private IAbpCliLoginAppService Service { get; set; }
        
        protected override async Task InternalExecuteAsync()
        {
            await Service.LogoutAsync();
        }
    }
}
