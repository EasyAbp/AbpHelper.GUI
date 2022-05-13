using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Proxy;
using EasyAbp.AbpHelper.Gui.AbpCli.Proxy.Dtos;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.AbpCli.Components.Proxy
{
    public partial class CSharpProxy
    {
        [Inject]
        private IAbpCliProxyAppService Service { get; set; }

        protected override async Task InternalExecuteAsync()
        {
            await Service.GenerateCSharpProxyAsync(Input);
        }

        protected virtual async Task InternalExecuteRemoveAsync()
        {
            await Service.RemoveCSharpProxyAsync(Input);
        }

        protected override void SetDirectoryToCurrentSolutionPath()
        {
            if (CurrentSolution.Value?.DirectoryPath is null)
            {
                Input.Directory = string.Empty;
                return;
            }

            var path = CurrentSolution.Value?.DirectoryPath;
            
            if (Directory.Exists(Path.Combine(CurrentSolution.Value?.DirectoryPath, "aspnet-core")))
            {
                path = path.SmartPathCombine("aspnet-core");
            }

            path = path.SmartPathCombine("src");

            path = Directory.GetDirectories(path).FirstOrDefault(x => x.EndsWith(".HttpApi.Client"));

            Input.Directory = path ?? string.Empty;
        }
        
        public virtual async Task ExecuteRemoveAsync()
        {
            try
            {
                var validate = true;
                if (ValidationsRef != null)
                {
                    validate = await ValidationsRef.ValidateAll();
                }
                if (validate)
                {
                    await InternalExecuteRemoveAsync();

                    await UiMessageService.Success(L[OperationSuccessfulMessage].Value, L[OperationSuccessfulTitle].Value);
                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }
    }
}
