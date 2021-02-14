using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.ModuleManagement.Installer.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.ModuleManagement.Installer
{
    public interface IModuleManagementInstallerAppService : IApplicationService
    {
        Task AddManyAsync(AddManyModuleInput input);

        Task RemoveManyAsync(RemoveManyModuleInput input);
    }
}