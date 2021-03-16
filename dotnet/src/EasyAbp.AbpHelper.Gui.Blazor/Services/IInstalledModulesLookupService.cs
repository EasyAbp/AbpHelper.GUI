using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Solutions.Dtos;

namespace EasyAbp.AbpHelper.Gui.Services
{
    public interface IInstalledModulesLookupService
    {
        Task<Dictionary<string, List<string>>> GetAsync(SolutionDto solutionDto);
    }
}