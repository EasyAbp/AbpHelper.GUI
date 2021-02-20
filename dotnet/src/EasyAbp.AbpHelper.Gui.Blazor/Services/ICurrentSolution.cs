using EasyAbp.AbpHelper.Gui.Solutions.Dtos;

namespace EasyAbp.AbpHelper.Gui.Blazor.Services
{
    public interface ICurrentSolution
    {
        SolutionDto Get();

        void Set(SolutionDto solutionDto);
    }
}