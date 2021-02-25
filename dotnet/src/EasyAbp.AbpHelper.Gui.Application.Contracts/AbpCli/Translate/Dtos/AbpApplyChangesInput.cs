using System;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.AbpCli.Translate.Dtos
{
    [Serializable]
    public class AbpApplyChangesInput : InputDtoWithRunningPath
    {
        public virtual bool Apply { get; } = true;
        
        [CanBeNull]
        public virtual string File { get; set; }

        public AbpApplyChangesInput()
        {
        }

        protected AbpApplyChangesInput([NotNull] string runningPath, [CanBeNull] string file) : base(runningPath)
        {
            File = file;
        }
    }
}