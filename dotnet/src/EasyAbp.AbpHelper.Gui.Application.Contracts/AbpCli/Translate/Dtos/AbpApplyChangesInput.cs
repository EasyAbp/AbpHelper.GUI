using System;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Translate.Dtos
{
    [Serializable]
    public class AbpApplyChangesInput : InputDtoWithDirectory
    {
        public virtual bool Apply { get; } = true;
        
        [CanBeNull]
        public virtual string File { get; set; }

        public AbpApplyChangesInput()
        {
        }

        public AbpApplyChangesInput([NotNull] string directory, [CanBeNull] string file) : base(directory)
        {
            File = file;
        }
    }
}