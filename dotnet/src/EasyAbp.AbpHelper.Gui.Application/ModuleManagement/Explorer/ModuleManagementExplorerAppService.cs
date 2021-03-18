using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.ModuleManagement.Explorer.Dtos;
using EasyAbp.AbpHelper.Gui.ModuleManagement.Installer.Dtos;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using Volo.Abp.Json;

namespace EasyAbp.AbpHelper.Gui.ModuleManagement.Explorer
{
    public class ModuleManagementExplorerAppService : ModuleManagementAppService, IModuleManagementExplorerAppService
    {
        private const int CacheExpirationMinutes = 60;
        private const string ModuleLibraryIndexUrl =
            "https://raw.githubusercontent.com/EasyAbp/ModuleLibrary/v1/index.json";

        private readonly IJsonSerializer _jsonSerializer;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDistributedCache<ModuleGroupsCacheItem> _cache;

        public ModuleManagementExplorerAppService(
            IJsonSerializer jsonSerializer,
            IHttpClientFactory httpClientFactory,
            IDistributedCache<ModuleGroupsCacheItem> cache)
        {
            _jsonSerializer = jsonSerializer;
            _httpClientFactory = httpClientFactory;
            _cache = cache;
        }
        
        public async Task<ListResultDto<ModuleGroupDto>> GetModuleGroupListAsync()
        {
            var item = await _cache.GetOrAddAsync("",
                async () => new ModuleGroupsCacheItem
                    {ModuleGroupList = await RequestModuleGroupListFromGithubRepositoryAsync()},
                () => new DistributedCacheEntryOptions
                    {AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CacheExpirationMinutes)});

            return new ListResultDto<ModuleGroupDto>(item.ModuleGroupList);
        }

        protected virtual async Task<List<ModuleGroupDto>> RequestModuleGroupListFromGithubRepositoryAsync()
        {
            var client = _httpClientFactory.CreateClient();

            var str = await client.GetStringAsync(ModuleLibraryIndexUrl);

            var groups = _jsonSerializer.Deserialize<ModuleLibraryIndexDto>(str).Groups;
            
            groups.Sort();

            return groups;
        }
    }
}