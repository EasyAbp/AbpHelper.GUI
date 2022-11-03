﻿using System;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.Shared.Dtos
{
    [Serializable]
    public abstract class AbpHelperInput : InputDtoWithDirectory
    {
        public virtual string Exclude { get; set; }

        public virtual string ProjectName { get; set; }

        public AbpHelperInput()
        {
        }

        public AbpHelperInput([NotNull] string directory, [CanBeNull] string projectName, [CanBeNull] string exclude) :
            base(directory)
        {
            Exclude = exclude;
            ProjectName = projectName;
        }
    }
}