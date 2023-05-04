using System;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Switch.Dtos
{
    [Serializable]
    public class AbpSwitchToLocalInput : InputDtoWithDirectory
    {
        public string Paths { get; set; }

        public AbpSwitchToLocalInput()
        {
        }

        public AbpSwitchToLocalInput([NotNull] string directory) : base(directory)
        {
        }
    }
}