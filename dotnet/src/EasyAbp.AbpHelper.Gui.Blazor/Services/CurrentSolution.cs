using EasyAbp.AbpHelper.Gui.Solutions.Dtos;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.AbpHelper.Gui.Blazor.Services
{
    public class CurrentSolution : ICurrentSolution, ISingletonDependency
    {
        private SolutionDto Solution { get; set; }

        public virtual SolutionDto Get()
        {
            return Solution;
        }

        public virtual void Set(SolutionDto solutionDto)
        {
            Solution = solutionDto;
        }
    }
}