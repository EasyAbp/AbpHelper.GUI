using System;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Switch.Dtos
{
    [Serializable]
    public class AbpSwitchToNightlyInput : InputDtoWithDirectory
    {
        public AbpSwitchToNightlyInput()
        {
        }

        public AbpSwitchToNightlyInput([NotNull] string directory) : base(directory)
        {
        }
    }
}