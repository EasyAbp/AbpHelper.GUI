using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Messages;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.Shared
{
    public abstract class ExecutableComponentBase : GuiComponentBase
    {
        protected string OperationSuccessfulTitle { get; set; } = "Congratulations";
        protected string OperationSuccessfulMessage { get; set; } = "OperationSuccessfulMessage";
        
        [Inject]
        protected IUiMessageService UiMessageService { get; set; }

        public virtual async Task ExecuteAsync()
        {
            await InternalExecuteAsync();

            await UiMessageService.Success(L[OperationSuccessfulMessage].Value, L[OperationSuccessfulTitle].Value);
        }

        protected abstract Task InternalExecuteAsync();
    }
}
