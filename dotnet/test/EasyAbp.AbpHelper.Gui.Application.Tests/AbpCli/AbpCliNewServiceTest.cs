using System.IO;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.AbpCli.New;
using EasyAbp.AbpHelper.Gui.AbpCli.New.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace EasyAbp.AbpHelper.Gui.AbpCli
{
    public class AbpCliNewServiceTest : GuiApplicationTestBase
    {
        protected const string SolutionName = "CreateAppTest";

        [Fact]
        public async Task Should_Create_App()
        {
            var service = ServiceProvider.GetRequiredService<IAbpCliNewAppService>();

            await service.CreateAppAsync(new AbpNewAppInput(
                SolutionName,
                GuiTestConsts.Folder,
                null,
                false,
                null,
                true,
                null,
                Database.SqlServer,
                null,
                false,
                true,
                AppUiFramework.Mvc,
                false,
                false,
                false,
                AppMobileApplicationFramework.None,
                AppDatabaseProvider.Ef,
                AbpThemes.LeptonxLite,
                false));

            File.Exists(Path.Combine(Path.Combine(GuiTestConsts.Folder, SolutionName), $"{SolutionName}.sln"))
                .ShouldBeTrue();
        }
    }
}