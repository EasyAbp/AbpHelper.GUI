using System;
using System.Threading.Tasks;
using Blazorise;
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
        
        protected Validations ValidationsRef;

        public virtual async Task ExecuteAsync()
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
                    await InternalExecuteAsync();

                    await UiMessageService.Success(L[OperationSuccessfulMessage].Value, L[OperationSuccessfulTitle].Value);
                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        protected abstract Task InternalExecuteAsync();
    }
}
