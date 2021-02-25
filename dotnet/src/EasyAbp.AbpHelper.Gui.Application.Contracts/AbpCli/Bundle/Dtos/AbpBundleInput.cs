using System;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Bundle.Dtos
{
    [Serializable]
    public class AbpBundleInput : InputDtoWithRunningPath
    {
        public virtual bool Force { get; set; }

        public AbpBundleInput()
        {
        }

        protected AbpBundleInput([NotNull] string runningPath, bool force) : base(runningPath)
        {
            Force = force;
        }
    }
}