using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Solutions.Dtos;

namespace EasyAbp.AbpHelper.Gui.Solutions
{
    public interface IRecentlySolutionsManager
    {
        Task<List<SolutionDto>> GetListAsync();
        
        Task UpdateListAsync(List<SolutionDto> input);
    }
}