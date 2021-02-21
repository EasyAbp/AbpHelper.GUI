using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.Update.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Update
{
    public interface IAbpCliUpdateAppService : IApplicationService
    {
        Task<ServiceExecutionResult> UpdateAsync(AbpUpdateInput input);
    }
}