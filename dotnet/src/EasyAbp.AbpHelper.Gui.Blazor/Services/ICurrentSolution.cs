using System;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Solutions.Dtos;

namespace EasyAbp.AbpHelper.Gui.Services
{
    public interface ICurrentSolution
    {
        SolutionDto Value { get; }

        Task SetAsync(SolutionDto solutionDto);
        
        event Func<Task> OnChangeAsync;
    }
}