using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Blazor.Models;

namespace EasyAbp.AbpHelper.Gui.Blazor.Services
{
    public interface IInstalledModulesLookupService
    {
        Task<Dictionary<string, List<string>>> GetAsync(Solution solution);
    }
}