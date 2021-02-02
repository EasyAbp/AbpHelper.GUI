using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Messages;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.Shared
{
    public abstract class ExecutableComponentBase : GuiComponentBase
    {
        protected string OperationSuccessfulTitle { get; set; } = "Congratulations";
        protected string OperationSuccessfulMessage { get; set; } = "OperationSuccessfulMessage";
        
        protected readonly IUiMessageService UiMessageService;

        public ExecutableComponentBase(IUiMessageService uiMessageService)
        {
            UiMessageService = uiMessageService;
        }
        
        public virtual async Task ExecuteAsync()
        {
            await ExecuteInternalAsync();

            await UiMessageService.Success(OperationSuccessfulMessage, OperationSuccessfulTitle);
        }

        protected abstract Task ExecuteInternalAsync();
    }
}
