using System;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Templates.Dtos;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.AbpHelper.Gui.Blazor.Services
{
    public class CurrentTemplate : ICurrentTemplate, ISingletonDependency
    {
        public TemplateDto Value { get; private set; }

        public virtual async Task SetAsync(TemplateDto templateDto)
        {
            Value = templateDto;

            if (OnChangeAsync != null)
            {
                await NotifyStateChanged();
            }
        }

        public event Func<Task> OnChangeAsync;

        private Task NotifyStateChanged() => OnChangeAsync?.Invoke();
    }
}