using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.UpdateCheck.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.UpdateCheck
{
    public interface IUpdateCheckAppService : IApplicationService
    {
        Task<UpdateCheckOutput> CheckAsync();
    }
}