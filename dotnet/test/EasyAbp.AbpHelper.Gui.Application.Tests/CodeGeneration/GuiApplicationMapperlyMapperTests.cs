using EasyAbp.AbpHelper.Core.Commands.Generate.Crud;
using EasyAbp.AbpHelper.Core.Commands.Generate.Methods;
using EasyAbp.AbpHelper.Gui.CodeGeneration.AppService.Dtos;
using EasyAbp.AbpHelper.Gui.CodeGeneration.Crud.Dtos;
using Shouldly;
using Volo.Abp.ObjectMapping;
using Xunit;

namespace EasyAbp.AbpHelper.Gui.CodeGeneration
{
    public class GuiApplicationMapperlyMapperTests : GuiApplicationTestBase
    {
        private readonly IObjectMapper _objectMapper;

        public GuiApplicationMapperlyMapperTests()
        {
            _objectMapper = GetRequiredService<IObjectMapper>();
        }

        [Fact]
        public void Should_Map_CrudInput_To_CommandOption_And_Split_Exclude()
        {
            // Arrange
            var input = new AbpHelperGenerateCrudInput
            {
                Directory = "/root",
                ProjectName = "Acme.BookStore",
                Exclude = "Foo Bar",
                Entity = "Book",
                SkipUi = true,
                NoOverwrite = true
            };

            // Act
            var option = _objectMapper.Map<AbpHelperGenerateCrudInput, CrudCommandOption>(input);

            // Assert
            option.Directory.ShouldBe("/root");
            option.ProjectName.ShouldBe("Acme.BookStore");
            option.Entity.ShouldBe("Book");
            option.SkipUi.ShouldBeTrue();
            option.NoOverwrite.ShouldBeTrue();
            option.Exclude.ShouldBe(new[] { "Foo", "Bar" });
        }

        [Fact]
        public void Should_Map_MethodsInput_And_Split_MethodNames_And_Exclude()
        {
            // Arrange
            var input = new AbpHelperGenerateAppServiceMethodsInput
            {
                Directory = "/root",
                ServiceName = "BookAppService",
                MethodNames = "Create Update",
                Exclude = "Bin Obj",
                IntegrationService = true
            };

            // Act
            var option = _objectMapper.Map<AbpHelperGenerateAppServiceMethodsInput, MethodsCommandOption>(input);

            // Assert
            option.ServiceName.ShouldBe("BookAppService");
            option.MethodNames.ShouldBe(new[] { "Create", "Update" });
            option.Exclude.ShouldBe(new[] { "Bin", "Obj" });
            option.IntegrationService.ShouldBeTrue();
        }
    }
}
