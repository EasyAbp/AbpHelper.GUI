using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Templates.Dtos;

namespace EasyAbp.AbpHelper.Gui.Templates
{
    public interface IRecentlyTemplatesManager
    {
        Task<List<TemplateDto>> GetListAsync();
        
        Task UpdateListAsync(List<TemplateDto> input);
    }
}