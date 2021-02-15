using EasyAbp.AbpHelper.Gui.Blazor.Models;

namespace EasyAbp.AbpHelper.Gui.Blazor.Services
{
    public interface ICurrentSolution
    {
        Solution Get();

        void Set(Solution solution);
    }
}