using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.LogService
{
    public interface ILogAppService : IApplicationService
    {
        Task<string> GetRecentLogFilePathAsync();
    }
}