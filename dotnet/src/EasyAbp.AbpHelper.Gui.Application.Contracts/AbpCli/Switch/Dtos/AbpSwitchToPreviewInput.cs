using System;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Switch.Dtos
{
    [Serializable]
    public class AbpSwitchToPreviewInput : InputDtoWithRunningPath
    {
        public AbpSwitchToPreviewInput()
        {
        }

        protected AbpSwitchToPreviewInput([NotNull] string runningPath) : base(runningPath)
        {
        }
    }
}