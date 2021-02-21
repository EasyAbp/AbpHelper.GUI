using System;
using EasyAbp.AbpHelper.Gui.Solutions.Dtos;

namespace EasyAbp.AbpHelper.Gui.Blazor.Services
{
    public interface ICurrentSolution
    {
        SolutionDto Value { get; }

        void Set(SolutionDto solutionDto);
        
        event Action OnChange;
    }
}