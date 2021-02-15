using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.ModuleManagement.Explorer.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.ModuleManagement.Explorer
{
    public interface IModuleManagementExplorerAppService : IApplicationService
    {
        Task<ListResultDto<ModuleGroupDto>> GetModuleGroupListAsync();
    }
}