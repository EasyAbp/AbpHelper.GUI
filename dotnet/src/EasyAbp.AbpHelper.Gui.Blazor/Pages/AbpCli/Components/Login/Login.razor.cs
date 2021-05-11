using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Login;
using EasyAbp.AbpHelper.Gui.AbpCli.Login.Dtos;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.AbpCli.Components.Login
{
    public partial class Login
    {
        [Inject]
        private IAbpCliLoginAppService Service { get; set; }

        protected AbpLoginInput Input { get; set; } = new();

        protected override async Task InternalExecuteAsync()
        {
            await Service.LoginAsync(Input);
        }
    }
}
