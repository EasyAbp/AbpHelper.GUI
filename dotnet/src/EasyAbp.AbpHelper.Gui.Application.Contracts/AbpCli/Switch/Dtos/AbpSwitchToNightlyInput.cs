using System;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Switch.Dtos
{
    [Serializable]
    public class AbpSwitchToNightlyInput : InputDtoWithRunningPath
    {
        public AbpSwitchToNightlyInput()
        {
        }

        protected AbpSwitchToNightlyInput([NotNull] string runningPath) : base(runningPath)
        {
        }
    }
}