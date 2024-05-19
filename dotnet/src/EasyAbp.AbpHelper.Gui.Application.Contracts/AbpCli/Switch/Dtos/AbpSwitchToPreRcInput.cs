using System;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Switch.Dtos
{
    [Serializable]
    public class AbpSwitchToPreRcInput : InputDtoWithDirectory
    {
        public AbpSwitchToPreRcInput()
        {
        }

        public AbpSwitchToPreRcInput([NotNull] string directory) : base(directory)
        {
        }
    }
}