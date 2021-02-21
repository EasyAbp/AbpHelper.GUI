using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Add.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Add
{
    public interface IAbpCliAddAppService : IApplicationService
    {
        Task<ServiceExecutionResult> AddPackageAsync(AbpAddPackageInput input);
        
        Task<ServiceExecutionResult> AddModuleAsync(AbpAddModuleInput input);
    }
}