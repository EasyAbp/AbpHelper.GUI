using System;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Solutions.Dtos;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.AbpHelper.Gui.Blazor.Services
{
    public class CurrentSolution : ICurrentSolution, ISingletonDependency
    {
        public SolutionDto Value { get; private set; }

        public virtual async Task SetAsync(SolutionDto solutionDto)
        {
            Value = solutionDto;

            if (OnChangeAsync != null)
            {
                await NotifyStateChanged();
            }
        }

        public event Func<Task> OnChangeAsync;

        private Task NotifyStateChanged() => OnChangeAsync?.Invoke();
    }
}