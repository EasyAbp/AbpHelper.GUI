using System.Threading.Tasks;

namespace EasyAbp.AbpHelper.Gui.LogService
{
    public interface ILogFilePathProvider
    {
        Task<string> GetRecentlyAsync();
    }
}