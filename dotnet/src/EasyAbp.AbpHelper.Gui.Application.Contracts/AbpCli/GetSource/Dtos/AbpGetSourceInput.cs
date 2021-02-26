using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.GetSource.Dtos
{
    [Serializable]
    public class AbpGetSourceInput : InputDtoWithDirectory
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

        public AbpGetSourceInput([NotNull] string directory, [NotNull] string moduleName, [CanBeNull] string version,
            [CanBeNull] string localFrameworkRef, bool preview) : base(directory)
        {
            ModuleName = moduleName;
            Version = version;
            LocalFrameworkRef = localFrameworkRef;
            Preview = preview;
        }
    }
}