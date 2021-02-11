using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace EasyAbp.AbpHelper.Gui.ModuleManagement.Explorer
{
    public class ModuleManagementExplorerAppServiceTests : GuiApplicationTestBase
    {
        private readonly IModuleManagementExplorerAppService _moduleManagementExplorerAppService;

        public ModuleManagementExplorerAppServiceTests()
        {
            _moduleManagementExplorerAppService = GetRequiredService<IModuleManagementExplorerAppService>();
        }

        [Fact]
        public async Task Should_Get_Module_Group_List()
        {
            // Arrange

            // Act
            var output = await _moduleManagementExplorerAppService.GetModuleGroupListAsync();

            // Assert
            output.Items.ShouldNotBeEmpty();
            output.Items.Select(x => x.Id).ShouldContain("EasyAbp.EShop");
        }
    }
}