using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.AbpHelper.Gui.LogService
{
    public class LogFilePathProvider : ILogFilePathProvider, ISingletonDependency
    {
        protected DirectoryInfo Directory { get; set; }
        
        public LogFilePathProvider()
        {
            Directory = new DirectoryInfo("Logs");
        }
        
        public virtual Task<string> GetRecentlyAsync()
        {
            return Task.FromResult(Directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First().FullName);
        }
    }
}