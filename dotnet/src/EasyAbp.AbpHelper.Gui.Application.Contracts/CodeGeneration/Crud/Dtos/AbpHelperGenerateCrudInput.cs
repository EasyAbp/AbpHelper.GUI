using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Shared.Dtos;
using JetBrains.Annotations;

namespace EasyAbp.AbpHelper.Gui.CodeGeneration.Crud.Dtos
{
    [Serializable]
    public class AbpHelperGenerateCrudInput : AbpHelperGenerateInput
    {
        [Required]
        [NotNull]
        public virtual string Entity { get; set; }

        [CanBeNull]
        public virtual string MigrationProjectName { get; set; }

        public virtual bool SkipPermissions { get; set; }

        public virtual bool SeparateDto { get; set; }

        public virtual bool EntityPrefixDto { get; set; }

        [CanBeNull]
        public virtual string DtoSuffix { get; set; }

        public virtual bool SkipCustomRepository { get; set; }

        public virtual bool SkipDbMigrations { get; set; }

        public virtual bool SkipUi { get; set; }

        public virtual bool SkipViewModel { get; set; }

        public virtual bool SkipLocalization { get; set; }

        public virtual bool SkipTest { get; set; }

        public virtual bool SkipEntityConstructors { get; set; }

        public AbpHelperGenerateCrudInput()
        {
        }
        
        public AbpHelperGenerateCrudInput([NotNull] string directory, [CanBeNull] string exclude, bool noOverwrite,
            [NotNull] string entity, [CanBeNull] string migrationProjectName, bool skipPermissions, bool separateDto,
            bool entityPrefixDto, [CanBeNull] string dtoSuffix, bool skipCustomRepository, bool skipDbMigrations,
            bool skipUi, bool skipViewModel, bool skipLocalization, bool skipTest, bool skipEntityConstructors) : base(
            directory, exclude, noOverwrite)
        {
            Entity = entity;
            MigrationProjectName = migrationProjectName;
            SkipPermissions = skipPermissions;
            SeparateDto = separateDto;
            EntityPrefixDto = entityPrefixDto;
            DtoSuffix = dtoSuffix;
            SkipCustomRepository = skipCustomRepository;
            SkipDbMigrations = skipDbMigrations;
            SkipUi = skipUi;
            SkipViewModel = skipViewModel;
            SkipLocalization = skipLocalization;
            SkipTest = skipTest;
            SkipEntityConstructors = skipEntityConstructors;
        }
    }
}