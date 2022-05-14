using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.AbpHelper.Gui.LogService
{
    public class LogFilePathProvider : ILogFilePathProvider, ISingletonDependency
    {
        private const string LogDirectoryPath = "Logs";
        private const string ErrorLogDirectoryPath = "Logs/Errors";
        
        protected DirectoryInfo LogDirectory { get; set; }
        
        protected DirectoryInfo ErrorLogDirectory { get; set; }
        
        public LogFilePathProvider()
        {
            Directory.CreateDirectory(LogDirectoryPath);
            Directory.CreateDirectory(ErrorLogDirectoryPath);
            
            LogDirectory = new DirectoryInfo(LogDirectoryPath);
            ErrorLogDirectory = new DirectoryInfo(ErrorLogDirectoryPath);
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