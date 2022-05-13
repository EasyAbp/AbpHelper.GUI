using System;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Proxy;
using Microsoft.AspNetCore.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.AbpCli.Components.Proxy
{
    public partial class AngularProxy
    {
        [Inject]
        private IAbpCliProxyAppService Service { get; set; }

        protected override async Task InternalExecuteAsync()
        {
            await Service.GenerateAngularProxyAsync(Input);
        }

        protected virtual async Task InternalExecuteRemoveAsync()
        {
            await Service.RemoveAngularProxyAsync(Input);
        }

        protected override void SetDirectoryToCurrentSolutionPath()
        {
            Input.Directory = CurrentSolution.Value?.DirectoryPath != null
                ? CurrentSolution.Value?.DirectoryPath.SmartPathCombine("angular")
                : string.Empty;
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
