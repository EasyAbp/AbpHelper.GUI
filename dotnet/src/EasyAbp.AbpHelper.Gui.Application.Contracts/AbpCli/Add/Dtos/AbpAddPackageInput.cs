using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Add.Dtos
{
    [Serializable]
    public class AbpAddPackageInput : InputDtoWithDirectory
    {
        [Required]
        [NotNull]
        public virtual string PackageName { get; set; }
        
        [CanBeNull]
        public virtual string Project { get; set; }

        public AbpAddPackageInput()
        {
        }

        public AbpAddPackageInput([NotNull] string directory, [NotNull] string packageName,
            [CanBeNull] string project) : base(directory)
        {
            PackageName = packageName;
            Project = project;
        }
    }
}