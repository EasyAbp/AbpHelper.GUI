using EasyAbp.AbpHelper.Gui.Templates.Dtos;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Volo.Abp.BlazoriseUI.Components;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.Templates
{
    public partial class Index
    {
        [Inject]
        private IStringLocalizer<AbpUiResource> UiLocalizer { get; set; }

        private DataGridEntityActionsColumn<TemplateDto> EntityActionsColumn { get; set; }

        protected virtual string GetDeleteConfirmationMessage(TemplateDto template)
        {
            return UiLocalizer["ItemWillBeDeletedMessage"];
        }
    }
}
