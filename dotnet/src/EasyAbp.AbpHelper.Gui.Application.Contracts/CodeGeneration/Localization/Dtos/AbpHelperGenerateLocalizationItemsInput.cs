using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.CodeGeneration.Localization.Dtos
{
    [Serializable]
    public class AbpHelperGenerateLocalizationItemsInput : AbpHelperGenerateInput
    {
        [Required]
        [NotNull]
        public virtual string Names { get; set; }

        public AbpHelperGenerateLocalizationItemsInput()
        {
        }

        public AbpHelperGenerateLocalizationItemsInput([NotNull] string directory, [CanBeNull] string exclude,
            bool noOverwrite, [NotNull] string names) : base(directory, exclude, noOverwrite)
        {
            Names = names;
        }
    }
}