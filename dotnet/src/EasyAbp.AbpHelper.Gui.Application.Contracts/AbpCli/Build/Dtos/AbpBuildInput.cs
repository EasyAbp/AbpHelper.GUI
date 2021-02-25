using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Build.Dtos
{
    [Serializable]
    public class AbpBuildInput : InputDtoWithRunningPath
    {
        [CanBeNull]
        public virtual string BuildName { get; set; }
        
        [CanBeNull]
        public virtual string DotnetBuildArguments { get; set; }
        
        public virtual bool Force { get; set; }

        public AbpBuildInput()
        {
        }

        protected AbpBuildInput([NotNull] string runningPath, [CanBeNull] string buildName,
            [CanBeNull] string dotnetBuildArguments, bool force) : base(runningPath)
        {
            BuildName = buildName;
            DotnetBuildArguments = dotnetBuildArguments;
            Force = force;
        }
    }
}