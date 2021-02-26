using System;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Switch.Dtos
{
    [Serializable]
    public class AbpSwitchToStableInput : InputDtoWithDirectory
    {
        public AbpSwitchToStableInput()
        {
        }

        protected AbpSwitchToStableInput([NotNull] string directory) : base(directory)
        {
        }
    }
}