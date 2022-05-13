using System;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Bundle.Dtos
{
    [Serializable]
    public class AbpBundleInput : InputDtoWithDirectory
    {
        public virtual bool Force { get; set; }

        public AbpBundleInput()
        {
        }

        public AbpBundleInput([NotNull] string directory, bool force) : base(directory)
        {
            Force = force;
        }
    }
}