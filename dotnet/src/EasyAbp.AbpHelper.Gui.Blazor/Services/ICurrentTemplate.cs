using System;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Templates.Dtos;

namespace EasyAbp.AbpHelper.Gui.Blazor.Services
{
    public interface ICurrentTemplate
    {
        TemplateDto Value { get; }

        Task SetAsync(TemplateDto templateDto);
        
        event Func<Task> OnChangeAsync;
    }
}