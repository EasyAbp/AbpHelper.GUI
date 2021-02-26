using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.CodeGeneration.AppService.Dtos
{
    [Serializable]
    public class AbpHelperGenerateAppServiceClassInput : AbpHelperGenerateInput
    {
        [Required]
        [NotNull]
        public virtual string Name { get; set; }

        [CanBeNull]
        public virtual string Folder { get; set; }


        public AbpHelperGenerateAppServiceClassInput()
        {
        }

        public AbpHelperGenerateAppServiceClassInput([NotNull] string directory, [CanBeNull] string exclude,
            bool noOverwrite, [NotNull] string name, [CanBeNull] string folder) : base(directory, exclude, noOverwrite)
        {
            Name = name;
            Folder = folder;
        }
    }
}