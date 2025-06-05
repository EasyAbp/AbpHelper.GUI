using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.Templates.Dtos
{
    public class TemplateDto
    {
        [Required]
        public string DisplayName { get; set; }

        public string DirectoryPath { get; set; }

        public bool Equals([CanBeNull] TemplateDto other)
        {
            if (other == null)
            {
                return false;
            }

            return DirectoryPath == other.DirectoryPath;
        }
    }
}