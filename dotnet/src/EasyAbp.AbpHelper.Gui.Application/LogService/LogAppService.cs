using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.LogService
{
    public class LogAppService : ApplicationService, ILogAppService
    {
        private readonly ILogFilePathProvider _logFilePathProvider;

        public LogAppService(ILogFilePathProvider logFilePathProvider)
        {
            _logFilePathProvider = logFilePathProvider;
        }
        
        public virtual async Task<string> GetRecentLogFilePathAsync()
        {
            return await _logFilePathProvider.GetRecentlyAsync();
        }
    }
}