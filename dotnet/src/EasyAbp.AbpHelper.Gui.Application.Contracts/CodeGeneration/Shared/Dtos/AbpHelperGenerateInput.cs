using System;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.CodeGeneration.Shared.Dtos
{
    [Serializable]
    public abstract class AbpHelperGenerateInput : AbpHelperInput
    {
        public virtual bool NoOverwrite { get; set; }

        public AbpHelperGenerateInput()
        {
            
        }
        
        public AbpHelperGenerateInput([NotNull] string directory, [CanBeNull] string exclude, bool noOverwrite) :
            base(directory, exclude)
        {
            NoOverwrite = noOverwrite;
        }
    }
}