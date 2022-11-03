using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Shared.Dtos;
using EasyAbp.AbpHelper.Gui.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.CodeGeneration.Migration.Dtos
{
    [Serializable]
    public partial class AbpHelperGenerateMigrationAddInput : AbpHelperInput
    {
        [Required]
        [NotNull]
        public virtual string Name { get; set; }

        [CanBeNull]
        public virtual string EfOptions { get; set; }

        [CanBeNull]
        public virtual string MigrationProjectName { get; set; }


        public AbpHelperGenerateMigrationAddInput()
        {
        }

        public AbpHelperGenerateMigrationAddInput([NotNull] string directory, [CanBeNull] string projectName,
            [CanBeNull] string exclude, [NotNull] string name, [CanBeNull] string efOptions,
            [CanBeNull] string migrationProjectName) : base(directory, projectName, exclude)
        {
            Name = name;
            EfOptions = efOptions;
            MigrationProjectName = migrationProjectName;
        }
    }
}