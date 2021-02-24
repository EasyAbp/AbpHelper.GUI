using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.Solutions.Dtos
{
    public class SolutionDto
    {
        public string DisplayName { get; set; }
        
        public SolutionType SolutionType { get; set; }
        
        public string DirectoryPath { get; set; }

        public bool Equals([CanBeNull] SolutionDto other)
        {
            if (other == null)
            {
                return false;
            }
            
            return DisplayName == other.DisplayName &&
                   SolutionType == other.SolutionType &&
                   DirectoryPath == other.DirectoryPath;
        }
    }
}