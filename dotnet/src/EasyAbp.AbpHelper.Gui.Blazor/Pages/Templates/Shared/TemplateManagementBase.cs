using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazorise;
using EasyAbp.AbpHelper.Gui.Blazor.Services;
using EasyAbp.AbpHelper.Gui.Localization;
using EasyAbp.AbpHelper.Gui.Templates;
using EasyAbp.AbpHelper.Gui.Templates.Dtos;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Web;

namespace EasyAbp.AbpHelper.Gui.Blazor.Pages.Templates.Shared
{
    public class TemplateManagementBase : GuiComponentBase, IDisposable
    {
        protected IReadOnlyList<TemplateDto> Templates = new List<TemplateDto>();

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected ICurrentTemplate CurrentTemplate { get; set; }

        [Inject]
        protected AbpBlazorMessageLocalizerHelper<GuiResource> Lh { get; set; }

        [Inject]
        protected ITemplateAppService TemplateAppService { get; set; }

        protected Modal Modal;

        protected Validations ValidationsRef;

        protected virtual TemplateDto CreateTemplate { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            await RefreshTemplates();
        }

        protected virtual async Task RefreshTemplates()
        {
            var templates = await TemplateAppService.GetListAsync();

            Templates = templates.Items;

            var targetTemplate = Templates.Count > 0 ? Templates[0] : null;

            if (targetTemplate == null && CurrentTemplate.Value != null ||
                targetTemplate != null && !targetTemplate.Equals(CurrentTemplate.Value))
            {
                await CurrentTemplate.SetAsync(targetTemplate);
            }
        }

        protected virtual async Task ChangeTemplateAsync(TemplateDto templateDto)
        {
            try
            {
                await TemplateAppService.UseAsync(templateDto);
            }
            finally
            {
                await RefreshTemplates();
            }
        }

        protected virtual Task OpenOpenTemplateModalAsync()
        {
            CreateTemplate = new TemplateDto();

            Modal.Show();

            return Task.CompletedTask;
        }

        protected virtual void CloseOpenTemplateModal()
        {
            Modal.Hide();
        }

        protected virtual async Task OpenTemplateExecuteAsync()
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
                    await TemplateAppService.UseAsync(CreateTemplate);

                    await Modal.Hide();

                    await RefreshTemplates();
                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        protected virtual async Task DeleteTemplateAsync(TemplateDto template)
        {
            await TemplateAppService.DeleteAsync(template);

            await RefreshTemplates();
        }

        protected override void OnInitialized()
        {
            CurrentTemplate.OnChangeAsync += StateHasChangedAsync;
        }

        public void Dispose()
        {
            CurrentTemplate.OnChangeAsync -= StateHasChangedAsync;
        }

        protected virtual Task StateHasChangedAsync()
        {
            StateHasChanged();

            return Task.CompletedTask;
        }
    }
}