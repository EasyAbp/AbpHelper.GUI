using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Templates.Dtos;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.Templates
{
    public interface ITemplateAppService : IApplicationService
    {
        Task<ListResultDto<TemplateDto>> GetListAsync();

        Task<TemplateDto> UseAsync(TemplateDto input);

        Task DeleteAsync(TemplateDto input);
    }
}