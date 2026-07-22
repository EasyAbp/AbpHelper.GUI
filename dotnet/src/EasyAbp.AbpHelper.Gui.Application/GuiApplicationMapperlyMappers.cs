using EasyAbp.AbpHelper.Core.Commands.Ef.Migrations.Add;
using EasyAbp.AbpHelper.Core.Commands.Ef.Migrations.Remove;
using EasyAbp.AbpHelper.Core.Commands.Generate.Controller;
using EasyAbp.AbpHelper.Core.Commands.Generate.Crud;
using EasyAbp.AbpHelper.Core.Commands.Generate.Localization;
using EasyAbp.AbpHelper.Core.Commands.Generate.Methods;
using EasyAbp.AbpHelper.Core.Commands.Generate.Service;
using EasyAbp.AbpHelper.Gui.CodeGeneration.AppService.Dtos;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Controller.Dtos;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Crud.Dtos;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Localization.Dtos;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Migration.Dtos;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EasyAbp.AbpHelper.Gui
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class AbpHelperGenerateCrudInputToCrudCommandOptionMapper
        : MapperBase<AbpHelperGenerateCrudInput, CrudCommandOption>
    {
        [MapperIgnoreTarget(nameof(CrudCommandOption.Exclude))]
        public override partial CrudCommandOption Map(AbpHelperGenerateCrudInput source);

        [MapperIgnoreTarget(nameof(CrudCommandOption.Exclude))]
        public override partial void Map(AbpHelperGenerateCrudInput source, CrudCommandOption destination);

        public override void AfterMap(AbpHelperGenerateCrudInput source, CrudCommandOption destination)
        {
            destination.Exclude = source.Exclude.SplitBySpace();
        }
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class AbpHelperGenerateAppServiceClassInputToServiceCommandOptionMapper
        : MapperBase<AbpHelperGenerateAppServiceClassInput, ServiceCommandOption>
    {
        [MapperIgnoreTarget(nameof(ServiceCommandOption.Exclude))]
        public override partial ServiceCommandOption Map(AbpHelperGenerateAppServiceClassInput source);

        [MapperIgnoreTarget(nameof(ServiceCommandOption.Exclude))]
        public override partial void Map(AbpHelperGenerateAppServiceClassInput source, ServiceCommandOption destination);

        public override void AfterMap(AbpHelperGenerateAppServiceClassInput source, ServiceCommandOption destination)
        {
            destination.Exclude = source.Exclude.SplitBySpace();
        }
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class AbpHelperGenerateAppServiceMethodsInputToMethodsCommandOptionMapper
        : MapperBase<AbpHelperGenerateAppServiceMethodsInput, MethodsCommandOption>
    {
        [MapperIgnoreTarget(nameof(MethodsCommandOption.Exclude))]
        [MapperIgnoreTarget(nameof(MethodsCommandOption.MethodNames))]
        public override partial MethodsCommandOption Map(AbpHelperGenerateAppServiceMethodsInput source);

        [MapperIgnoreTarget(nameof(MethodsCommandOption.Exclude))]
        [MapperIgnoreTarget(nameof(MethodsCommandOption.MethodNames))]
        public override partial void Map(AbpHelperGenerateAppServiceMethodsInput source, MethodsCommandOption destination);

        public override void AfterMap(AbpHelperGenerateAppServiceMethodsInput source, MethodsCommandOption destination)
        {
            destination.Exclude = source.Exclude.SplitBySpace();
            destination.MethodNames = source.MethodNames.SplitBySpace();
        }
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class AbpHelperGenerateControllerInputToControllerCommandOptionMapper
        : MapperBase<AbpHelperGenerateControllerInput, ControllerCommandOption>
    {
        [MapperIgnoreTarget(nameof(ControllerCommandOption.Exclude))]
        public override partial ControllerCommandOption Map(AbpHelperGenerateControllerInput source);

        [MapperIgnoreTarget(nameof(ControllerCommandOption.Exclude))]
        public override partial void Map(AbpHelperGenerateControllerInput source, ControllerCommandOption destination);

        public override void AfterMap(AbpHelperGenerateControllerInput source, ControllerCommandOption destination)
        {
            destination.Exclude = source.Exclude.SplitBySpace();
        }
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class AbpHelperGenerateLocalizationItemsInputToLocalizationCommandOptionMapper
        : MapperBase<AbpHelperGenerateLocalizationItemsInput, LocalizationCommandOption>
    {
        [MapperIgnoreTarget(nameof(LocalizationCommandOption.Exclude))]
        [MapperIgnoreTarget(nameof(LocalizationCommandOption.Names))]
        public override partial LocalizationCommandOption Map(AbpHelperGenerateLocalizationItemsInput source);

        [MapperIgnoreTarget(nameof(LocalizationCommandOption.Exclude))]
        [MapperIgnoreTarget(nameof(LocalizationCommandOption.Names))]
        public override partial void Map(AbpHelperGenerateLocalizationItemsInput source, LocalizationCommandOption destination);

        public override void AfterMap(AbpHelperGenerateLocalizationItemsInput source, LocalizationCommandOption destination)
        {
            destination.Exclude = source.Exclude.SplitBySpace();
            destination.Names = source.Names.SplitBySpace();
        }
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class AbpHelperGenerateMigrationAddInputToAddCommandOptionMapper
        : MapperBase<AbpHelperGenerateMigrationAddInput, AddCommandOption>
    {
        [MapperIgnoreTarget(nameof(AddCommandOption.Exclude))]
        [MapperIgnoreTarget(nameof(AddCommandOption.EfOptions))]
        public override partial AddCommandOption Map(AbpHelperGenerateMigrationAddInput source);

        [MapperIgnoreTarget(nameof(AddCommandOption.Exclude))]
        [MapperIgnoreTarget(nameof(AddCommandOption.EfOptions))]
        public override partial void Map(AbpHelperGenerateMigrationAddInput source, AddCommandOption destination);

        public override void AfterMap(AbpHelperGenerateMigrationAddInput source, AddCommandOption destination)
        {
            destination.Exclude = source.Exclude.SplitBySpace();
            destination.EfOptions = source.EfOptions.SplitBySpace();
        }
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class AbpHelperGenerateMigrationRemoveInputToRemoveCommandOptionMapper
        : MapperBase<AbpHelperGenerateMigrationRemoveInput, RemoveCommandOption>
    {
        [MapperIgnoreTarget(nameof(RemoveCommandOption.Exclude))]
        [MapperIgnoreTarget(nameof(RemoveCommandOption.EfOptions))]
        public override partial RemoveCommandOption Map(AbpHelperGenerateMigrationRemoveInput source);

        [MapperIgnoreTarget(nameof(RemoveCommandOption.Exclude))]
        [MapperIgnoreTarget(nameof(RemoveCommandOption.EfOptions))]
        public override partial void Map(AbpHelperGenerateMigrationRemoveInput source, RemoveCommandOption destination);

        public override void AfterMap(AbpHelperGenerateMigrationRemoveInput source, RemoveCommandOption destination)
        {
            destination.Exclude = source.Exclude.SplitBySpace();
            destination.EfOptions = source.EfOptions.SplitBySpace();
        }
    }
}
