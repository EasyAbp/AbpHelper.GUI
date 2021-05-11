using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Proxy;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.AbpCli.Components.Proxy
{
    public partial class GenerateProxy
    {
        [Inject]
        private IAbpCliProxyAppService Service { get; set; }

        protected override async Task InternalExecuteAsync()
        {
            await Service.GenerateProxyAsync(Input);
        }

        protected override void SetDirectoryToCurrentSolutionPath()
        {
            Input.Directory = CurrentSolution.Value?.DirectoryPath != null
                ? CurrentSolution.Value?.DirectoryPath.SmartPathCombine("angular")
                : string.Empty;
        }
    }
}
