using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Solutions.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.Solutions
{
    public class SolutionAppService : ApplicationService, ISolutionAppService
    {
        private const byte RecentlySolutionsMaxCount = 10;
        
        private readonly IRecentlySolutionsManager _manager;

        public SolutionAppService(IRecentlySolutionsManager manager)
        {
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
                list.AddFirst(input);
            }
            else
            {
                list.MoveItem((x) => x == solution, 0);
            }

            if (list.Count > RecentlySolutionsMaxCount)
            {
                list = list.GetRange(0, RecentlySolutionsMaxCount);
            }

            await _manager.UpdateListAsync(list);

            return input;
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
    }
}