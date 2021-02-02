using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Services.AbpCli.New.Dtos;
using EasyAbp.AbpHelper.Gui.Services.Shared;
using EasyAbp.AbpHelper.Gui.Services.Shared.Dtos;
using Volo.Abp;

namespace EasyAbp.AbpHelper.Gui.Services.AbpCli.New
{
    public interface IAbpCliNewService : IRemoteService
    {
        Task<ServiceExecutionResult> CreateAppAsync(AbpNewAppInput input);
    }
}