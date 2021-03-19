using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.ModuleManagement.Explorer.Dtos;

namespace EasyAbp.AbpHelper.Gui.ModuleManagement.Explorer
{
    public interface IModuleLibrarySourcesManager
    {
        Task<List<ModuleLibrarySourceDto>> GetListAsync();
        
        Task UpdateListAsync(List<ModuleLibrarySourceDto> input);
    }
}