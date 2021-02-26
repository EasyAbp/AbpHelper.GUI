using System;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Switch.Dtos
{
    [Serializable]
    public class AbpSwitchToPreviewInput : InputDtoWithDirectory
    {
        public AbpSwitchToPreviewInput()
        {
        }

        protected AbpSwitchToPreviewInput([NotNull] string directory) : base(directory)
        {
        }
    }
}