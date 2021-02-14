using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.ModuleManagement.Installer.Dtos;
using Xunit;

namespace EasyAbp.AbpHelper.Gui.ModuleManagement.Installer
{
    public class ModuleManagementInstallerAppServiceTests : GuiApplicationTestBase
    {
        private readonly IModuleManagementInstallerAppService _moduleManagementInstallerAppService;

        public ModuleManagementInstallerAppServiceTests()
        {
            _moduleManagementInstallerAppService = GetRequiredService<IModuleManagementInstallerAppService>();
        }

        [Fact]
        public async Task Should_Add_Module()
        {
            // Arrange

            // Act
            // await _moduleManagementInstallerAppService.AddManyAsync(new AddManyModuleInput
            // {
            //     DirectoryPath = "C:\\Temp\\TestApp",
            //     InstallationInfos = new List<AddManyModuleInputInstallationInfo>
            //     {
            //         new()
            //         {
            //             ModuleGroupId = "EasyAbp.EShop",
            //             ModuleId = "Orders.Application",
            //             Targets = new [] {"a"}
            //         }
            //     }
            // });

            // Assert
        }
        
        [Fact]
        public async Task Should_Remove_Module()
        {
            // Arrange

            // Act
            // await _moduleManagementInstallerAppService.RemoveManyAsync(new RemoveManyModuleInput()
            // {
            //     DirectoryPath = "C:\\Temp\\TestApp",
            //     InstallationInfos = new List<RemoveManyModuleInputInstallationInfo>
            //     {
            //         new()
            //         {
            //             ModuleGroupId = "EasyAbp.EShop",
            //             ModuleId = "Orders.Application",
            //             Targets = new [] {"a"}
            //         }
            //     }
            // });

            // Assert
        }
    }
}