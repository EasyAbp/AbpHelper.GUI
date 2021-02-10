using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.New.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp;

namespace EasyAbp.AbpHelper.Gui.AbpCli.New
{
    public interface IAbpCliNewService : IRemoteService
    {
        Task<ServiceExecutionResult> CreateAppAsync(AbpNewAppInput input);
    }
}