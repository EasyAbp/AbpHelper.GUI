using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.Shared.Dtos
{
    public class InputDtoWithDirectory
    {
        [Required]
        [NotNull]
        public virtual string Directory { get; set; }

        [CanBeNull]
        public virtual string TemplatePath { get; set; }

        protected InputDtoWithDirectory()
        {

        }

        public InputDtoWithDirectory([NotNull] string directory)
        {
            Directory = directory;
        }
    }
}