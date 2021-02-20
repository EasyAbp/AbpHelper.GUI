using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Solutions.Dtos;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.AbpHelper.Gui.Blazor.Services
{
    public class InstalledModulesLookupService : IInstalledModulesLookupService, ITransientDependency
    {
        public async Task<Dictionary<string, List<string>>> GetAsync(SolutionDto solutionDto)
        {
            return new()
            {
                {"Application", new List<string>()},
                {"Application.Contracts", new List<string>()},
                {"Domain", new List<string>()},
                {"Domain.Shared", new List<string>()},
                {"EntityFrameworkCore", new List<string>()},
                {"HttpApi", new List<string>()},
                {"HttpApi.Client", new List<string>()},
                {"Web", new List<string>()}
            };
        }
    }
}