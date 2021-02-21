using System;
using EasyAbp.AbpHelper.Gui.Solutions.Dtos;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.AbpHelper.Gui.Blazor.Services
{
    public class CurrentSolution : ICurrentSolution, ISingletonDependency
    {
        public SolutionDto Value { get; private set; }

        public virtual void Set(SolutionDto solutionDto)
        {
            Value = solutionDto;

            NotifyStateChanged();
        }

        public event Action OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}