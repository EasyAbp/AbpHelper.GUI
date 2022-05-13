using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Translate.Dtos
{
    [Serializable]
    public class AbpCreateTranslationFileInput : InputDtoWithDirectory
    {
        [Required]
        [NotNull]
        public virtual string Culture { get; set; }
        
        [CanBeNull]
        public virtual string ReferenceCulture { get; set; }
        
        [CanBeNull]
        public virtual string Output { get; set; }
        
        public virtual bool AllValues { get; set; }

        public AbpCreateTranslationFileInput()
        {
        }

        public AbpCreateTranslationFileInput([NotNull] string directory, [NotNull] string culture,
            [CanBeNull] string referenceCulture, [CanBeNull] string output, bool allValues) : base(directory)
        {
            Culture = culture;
            ReferenceCulture = referenceCulture;
            Output = output;
            AllValues = allValues;
        }
    }
}