using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.ModuleManagement.Explorer.Dtos;
using EasyAbp.AbpHelper.Gui.ModuleManagement.Installer.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Json;

namespace EasyAbp.AbpHelper.Gui.ModuleManagement.Explorer
{
    public class ModuleManagementExplorerAppService : ModuleManagementAppService, IModuleManagementExplorerAppService
    {
        private const string ModuleLibraryIndexUrl =
            "https://raw.githubusercontent.com/EasyAbp/ModuleLibrary/main/index.json";

        private readonly IJsonSerializer _jsonSerializer;
        private readonly IHttpClientFactory _httpClientFactory;

        public ModuleManagementExplorerAppService(
            IJsonSerializer jsonSerializer,
            IHttpClientFactory httpClientFactory)
        {
            _jsonSerializer = jsonSerializer;
            _httpClientFactory = httpClientFactory;
        }
        
        public async Task<ListResultDto<ModuleGroupDto>> GetModuleGroupListAsync()
        {
            var client = _httpClientFactory.CreateClient();

            var str = await client.GetStringAsync(ModuleLibraryIndexUrl);

            return new ListResultDto<ModuleGroupDto>(_jsonSerializer.Deserialize<ModuleLibraryIndexDto>(str).Groups);
        }
    }
}