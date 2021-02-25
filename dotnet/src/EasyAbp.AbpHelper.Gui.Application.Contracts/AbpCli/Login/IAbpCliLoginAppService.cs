using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Login.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Login
{
    public interface IAbpCliLoginAppService : IApplicationService
    {
        Task<ServiceExecutionResult> LoginAsync(AbpLoginInput input);
        
        Task<ServiceExecutionResult> LogoutAsync();
    }
}