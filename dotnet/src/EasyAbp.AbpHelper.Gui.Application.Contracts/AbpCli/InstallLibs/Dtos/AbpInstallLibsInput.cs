using System;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.InstallLibs.Dtos
{
    [Serializable]
    public class AbpInstallLibsInput : InputDtoWithDirectory
    {
        public AbpInstallLibsInput()
        {
        }

        public AbpInstallLibsInput([NotNull] string directory) : base(directory)
        {
        }
    }
}