using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.GetSource.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.AbpCli.GetSource
{
    public interface IAbpCliGetSourceAppService : IApplicationService
    {
        Task<ServiceExecutionResult> GetSourceAsync(AbpGetSourceInput input);
    }
}