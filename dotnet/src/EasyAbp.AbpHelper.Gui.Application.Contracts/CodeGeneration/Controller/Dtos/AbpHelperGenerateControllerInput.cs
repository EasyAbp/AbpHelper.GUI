using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.CodeGeneration.Controller.Dtos
{
    [Serializable]
    public class AbpHelperGenerateControllerInput : AbpHelperGenerateInput
    {
        [Required]
        [NotNull]
        public virtual string Name { get; set; }

        public virtual bool SkipBuild { get; set; }


        public AbpHelperGenerateControllerInput()
        {
        }

        public AbpHelperGenerateControllerInput([NotNull] string directory, [CanBeNull] string exclude,
            bool noOverwrite, [NotNull] string name, bool skipBuild) : base(directory, exclude, noOverwrite)
        {
            Name = name;
            SkipBuild = skipBuild;
        }
    }
}