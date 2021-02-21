using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.Shared.Dtos
{
    public class InputDtoWithRunningPath
    {
        [Required]
        [NotNull]
        public virtual string RunningPath { get; set; }

        protected InputDtoWithRunningPath()
        {
            
        }
        
        public InputDtoWithRunningPath([NotNull] string runningPath)
        {
            RunningPath = runningPath;
        }
    }
}