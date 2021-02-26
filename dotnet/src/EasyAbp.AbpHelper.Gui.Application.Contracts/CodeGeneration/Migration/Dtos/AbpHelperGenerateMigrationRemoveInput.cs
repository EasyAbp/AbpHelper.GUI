using System;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Shared.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.CodeGeneration.Migration.Dtos
{
    [Serializable]
    public class AbpHelperGenerateMigrationRemoveInput : AbpHelperInput
    {
        [CanBeNull]
        public virtual string EfOptions { get; set; }
        
        [CanBeNull]
        public virtual string MigrationProjectName { get; set; }


        public AbpHelperGenerateMigrationRemoveInput()
        {
        }

        public AbpHelperGenerateMigrationRemoveInput([NotNull] string directory, [CanBeNull] string exclude,
            [CanBeNull] string efOptions, [CanBeNull] string migrationProjectName) : base(directory, exclude)
        {
            EfOptions = efOptions;
            MigrationProjectName = migrationProjectName;
        }
    }
}