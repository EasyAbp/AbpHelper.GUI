using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Core.Services;
using EasyAbp.AbpHelper.Gui.Solutions.Dtos;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.Solutions
{
    public class SolutionAppService : ApplicationService, ISolutionAppService
    {
        private const byte RecentlySolutionsMaxCount = 10;

        private readonly IListPackageService _listPackageService;
        private readonly IRecentlySolutionsManager _manager;

        public SolutionAppService(
            IListPackageService listPackageService,
            IRecentlySolutionsManager manager)
        {
            _listPackageService = listPackageService;
            _manager = manager;
        }
        
        public virtual async Task<ListResultDto<SolutionDto>> GetListAsync()
        {
            return new(await _manager.GetListAsync());
        }

        public virtual async Task<SolutionDto> UseAsync(SolutionDto input)
        {
            Check.NotNullOrWhiteSpace(input.DisplayName, nameof(input.DisplayName));
            Check.NotNullOrWhiteSpace(input.DirectoryPath, nameof(input.DirectoryPath));
            
            var list = await _manager.GetListAsync();

            var solution = FindSolution(list, input);
            
            if (solution == null)
            {
                solution = input;
                
                list.AddFirst(solution);
            }
            else
            {
                list.MoveItem((x) => x == solution, 0);
            }

            if (!await IsSolutionDirectoryValidAsync(solution))
            {
                list.RemoveAt(0);
                
                await UpdateRecentlySolutionListAsync(list);

                throw new BusinessException("Gui:InvalidSolutionDirectoryPath");
            }

            await UpdateRecentlySolutionListAsync(list);

            return input;
        }

        protected virtual async Task UpdateRecentlySolutionListAsync(List<SolutionDto> list)
        {
            if (list.Count > RecentlySolutionsMaxCount)
            {
                list = list.GetRange(0, RecentlySolutionsMaxCount);
            }

            await _manager.UpdateListAsync(list);
        }

        protected virtual Task<bool> IsSolutionDirectoryValidAsync(SolutionDto solution)
        {
            return Task.FromResult(Directory.Exists(solution.DirectoryPath));
        }

        protected virtual SolutionDto FindSolution(IEnumerable<SolutionDto> solutions, SolutionDto target)
        {
            return solutions.FirstOrDefault(x =>
                x.DisplayName == target.DisplayName &&
                x.SolutionType == target.SolutionType &&
                x.DirectoryPath == target.DirectoryPath);
        }

        public virtual async Task DeleteAsync(SolutionDto input)
        {
            var list = await _manager.GetListAsync();

            FindSolution(list, input);

            await _manager.UpdateListAsync(list);
        }

        public virtual async Task<GetPackageDictionaryOutput> GetPackageDictionaryAsync(GetPackageDictionaryInput input)
        {
            var result = await _listPackageService.GetInstalledPackagesAsync(input.DirectoryPath);

            return new GetPackageDictionaryOutput
            {
                SolutionName = result.SolutionName,
                Items = new Dictionary<string, List<PackageInfo>>(result.Items.Select(x =>
                    new KeyValuePair<string, List<PackageInfo>>(x.Key,
                        x.Value.Select(y => new PackageInfo(y.Name, y.Version)).ToList())))
            };
        }
    }
}