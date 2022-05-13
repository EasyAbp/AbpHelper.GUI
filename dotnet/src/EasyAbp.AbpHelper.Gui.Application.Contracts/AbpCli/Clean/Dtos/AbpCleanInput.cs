using System;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Clean.Dtos
{
    [Serializable]
    public class AbpCleanInput : InputDtoWithDirectory
    {
        public AbpCleanInput()
        {
        }

        public AbpCleanInput([NotNull] string directory) : base(directory)
        {
        }
    }
}