using System.IO;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.Services.AbpCli.New;
using EasyAbp.AbpHelper.Gui.Services.AbpCli.New.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace EasyAbp.AbpHelper.Gui.AbpCli
{
    public class AbpCliNewServiceTest : GuiTestBase<GuiTestBaseModule>
    {
        protected const string SolutionName = "CreateAppTest";
        
        [Fact]
        public async Task Should_Create_App()
        {
            var service = ServiceProvider.GetRequiredService<IAbpCliNewService>();

            await service.CreateAppAsync(new AbpNewAppInput(
                SolutionName, 
                GuiTestConsts.Folder,
                null,
                false,
                null,
                false,
                null,
                Database.SqlServer,
                null,
                false,
                AppUiFramework.Mvc,
                false,
                false,
                AppMobileApplicationFramework.None,
                AppDatabaseProvider.Ef));

            File.Exists(Path.Combine(Path.Combine(GuiTestConsts.Folder, SolutionName), $"{SolutionName}.sln")).ShouldBeTrue();
        }
    }
}