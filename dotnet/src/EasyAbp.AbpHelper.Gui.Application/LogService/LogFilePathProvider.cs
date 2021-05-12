using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.AbpHelper.Gui.LogService
{
    public class LogFilePathProvider : ILogFilePathProvider, ISingletonDependency
    {
        protected DirectoryInfo LogDirectory { get; set; }
        
        protected DirectoryInfo ErrorLogDirectory { get; set; }
        
        public LogFilePathProvider()
        {
            LogDirectory = new DirectoryInfo("Logs");
            ErrorLogDirectory = new DirectoryInfo("Logs/Errors");
        }
        
        public virtual Task<string> GetRecentLogPathAsync()
        {
            return Task.FromResult(LogDirectory.GetFiles().OrderByDescending(f => f.LastWriteTime).FirstOrDefault()?.FullName);
        }

        public virtual Task<string> GetRecentErrorLogPathAsync()
        {
            return Task.FromResult(ErrorLogDirectory.GetFiles().OrderByDescending(f => f.LastWriteTime).FirstOrDefault()?.FullName);
        }
    }
}