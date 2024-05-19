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

        public virtual bool Online { get; set; }

        [CanBeNull]
        public virtual string DeeplAuthKey { get; set; }

        public AbpCreateTranslationFileInput()
        {
        }

        public AbpCreateTranslationFileInput([NotNull] string culture, [CanBeNull] string referenceCulture,
            [CanBeNull] string output, bool allValues, bool online, [CanBeNull] string deeplAuthKey)
        {
            Culture = culture;
            ReferenceCulture = referenceCulture;
            Output = output;
            AllValues = allValues;
            Online = online;
            DeeplAuthKey = deeplAuthKey;
        }
    }
}