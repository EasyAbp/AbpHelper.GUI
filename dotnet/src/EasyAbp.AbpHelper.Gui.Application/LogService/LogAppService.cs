using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EasyAbp.AbpHelper.Gui.LogService
{
    public class LogAppService : ApplicationService, ILogAppService
    {
        public virtual Task<string> GetRecentLogFilePathAsync()
        {
            var directory = new DirectoryInfo("Logs");

            return Task.FromResult(directory.GetFiles()
                .OrderByDescending(f => f.LastWriteTime)
                .First().FullName);
        }
    }
}