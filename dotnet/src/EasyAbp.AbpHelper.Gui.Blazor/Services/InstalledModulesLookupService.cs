using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Solutions;
using EasyAbp.AbpHelper.Gui.Solutions.Dtos;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.AbpHelper.Gui.Blazor.Services
{
    public class InstalledModulesLookupService : IInstalledModulesLookupService, ITransientDependency
    {
        private readonly ISolutionAppService _solutionAppService;

        public InstalledModulesLookupService(ISolutionAppService solutionAppService)
        {
            _solutionAppService = solutionAppService;
        }
        
        public virtual async Task<Dictionary<string, List<string>>> GetAsync(SolutionDto solutionDto)
        {
            var output = await _solutionAppService.GetPackageDictionaryAsync(new GetPackageDictionaryInput
            {
                DirectoryPath = solutionDto.DirectoryPath
            });
            
            var str = $"{output.SolutionName}.";

            return new Dictionary<string, List<string>>(output.Items.Where(x => x.Key.StartsWith(str)).Select(x =>
                new KeyValuePair<string, List<string>>(x.Key.Substring(str.Length),
                    x.Value.Select(y => y.Name).ToList())));
        }
    }
}