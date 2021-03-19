using System;
using System.Collections.Generic;
using System.Linq;
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

        private readonly IJsonSerializer _jsonSerializer;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IModuleLibrarySourcesManager _moduleLibrarySourcesManager;
        private readonly IDistributedCache<ModuleGroupsCacheItem> _cache;

        public ModuleManagementExplorerAppService(
            IJsonSerializer jsonSerializer,
            IHttpClientFactory httpClientFactory,
            IModuleLibrarySourcesManager moduleLibrarySourcesManager,
            IDistributedCache<ModuleGroupsCacheItem> cache)
        {
            _jsonSerializer = jsonSerializer;
            _httpClientFactory = httpClientFactory;
            _moduleLibrarySourcesManager = moduleLibrarySourcesManager;
            _cache = cache;
        }
        
        public virtual async Task<ListResultDto<ModuleGroupDto>> GetModuleGroupListAsync()
        {
            var groupIdGroupMapping = new Dictionary<string, ModuleGroupDto>();

            var sources = await _moduleLibrarySourcesManager.GetListAsync();

            foreach (var source in sources)
            {
                foreach (var group in await GetModuleGroupList(source))
                {
                    if (!groupIdGroupMapping.ContainsKey(group.Id))
                    {
                        groupIdGroupMapping.Add(group.Id, group);
                    }
                }
            }

            var groups = groupIdGroupMapping.Values.ToList();

            groups.Sort();

            return new ListResultDto<ModuleGroupDto>(groups);
        }

        protected virtual async Task<List<ModuleGroupDto>> GetModuleGroupList(ModuleLibrarySourceDto source)
        {
            var item = await _cache.GetOrAddAsync(source.Name,
                async () => new ModuleGroupsCacheItem
                    {ModuleGroupList = await RequestModuleGroupListFromSourceAsync(source)},
                () => new DistributedCacheEntryOptions
                    {AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CacheExpirationMinutes)});

            return item.ModuleGroupList;
        }

        protected virtual async Task<List<ModuleGroupDto>> RequestModuleGroupListFromSourceAsync(ModuleLibrarySourceDto source)
        {
            var client = _httpClientFactory.CreateClient();

            var str = await client.GetStringAsync(source.IndexUrl);

            return _jsonSerializer.Deserialize<ModuleLibraryIndexDto>(str).Groups;
        }
    }
}