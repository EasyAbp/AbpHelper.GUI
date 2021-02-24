using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Solutions.Dtos;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.Solutions
{
    public interface ISolutionAppService : IApplicationService
    {
        Task<ListResultDto<SolutionDto>> GetListAsync();

        Task<SolutionDto> UseAsync(SolutionDto input);

        Task DeleteAsync(SolutionDto input);

        Task<GetPackageDictionaryOutput> GetPackageDictionaryAsync(GetPackageDictionaryInput input);
    }
}