using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.ModuleManagement.Explorer.dtos;
using EasyAbp.AbpHelper.Gui.ModuleManagement.Installer.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.ModuleManagement.Explorer
{
    public interface IModuleManagementExplorerAppService : IApplicationService
    {
        Task<ListResultDto<ModuleGroupDto>> GetModuleGroupListAsync();
    }
}