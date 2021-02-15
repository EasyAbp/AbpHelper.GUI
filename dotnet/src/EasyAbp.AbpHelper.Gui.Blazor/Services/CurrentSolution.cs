using EasyAbp.AbpHelper.Gui.Blazor.Models;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.AbpHelper.Gui.Blazor.Services
{
    public class CurrentSolution : ICurrentSolution, ISingletonDependency
    {
        private Solution _solution = new()
        {
            DisplayName = "TestApp",
            SolutionType = SolutionType.Application,
            DirectoryPath = "C:\\Temp\\TestApp"
        };
        
        public virtual Solution Get()
        {
            return _solution;
        }

        public virtual void Set(Solution solution)
        {
            _solution = solution;
        }
    }
}