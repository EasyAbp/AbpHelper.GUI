using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.ModuleManagement.Installer;
using EasyAbp.AbpHelper.Gui.ModuleManagement.Installer.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.AbpHelper.Gui.Controllers.ModuleManagement
{
    [RemoteService]
    [Route("/api/abp-helper/module-management/installer")]
    public class ModuleManagementInstallerController : GuiController, IModuleManagementInstallerAppService
    {
        private readonly IModuleManagementInstallerAppService _service;

        public ModuleManagementInstallerController(IModuleManagementInstallerAppService service)
        {
            _service = service;
        }
        
        [HttpPost]
        [Route("add")]
        public Task AddManyAsync(AddManyModuleInput input)
        {
            return _service.AddManyAsync(input);
        }

        [HttpPost]
        [Route("remove")]
        public Task RemoveManyAsync(RemoveManyModuleInput input)
        {
            return _service.RemoveManyAsync(input);
        }
    }
}