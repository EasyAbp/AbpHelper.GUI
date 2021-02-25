using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.GetSource.Dtos
{
    [Serializable]
    public class AbpGetSourceInput : InputDtoWithRunningPath
    {
        [Required]
        [NotNull]
        public virtual string ModuleName { get; set; }
        
        [CanBeNull]
        public virtual string Version { get; set; }
        
        [CanBeNull]
        public virtual string LocalFrameworkRef { get; set; }
        
        public virtual bool Preview { get; set; }

        public AbpGetSourceInput()
        {
        }

        public AbpGetSourceInput([NotNull] string runningPath, [NotNull] string moduleName, [CanBeNull] string version,
            [CanBeNull] string localFrameworkRef, bool preview) : base(runningPath)
        {
            ModuleName = moduleName;
            Version = version;
            LocalFrameworkRef = localFrameworkRef;
            Preview = preview;
        }
    }
}