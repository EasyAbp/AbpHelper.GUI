using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Gui.UpdateCheck.Dtos;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace EasyAbp.AbpHelper.Gui.UpdateCheck
{
    public class UpdateCheckAppService : ApplicationService, IUpdateCheckAppService
    {
        private const string LatestReleaseUri = "https://api.github.com/repos/EasyAbp/AbpHelper.GUI/releases/latest";
        private const int CacheExpirationMinutes = 10;

        private readonly IJsonSerializer _jsonSerializer;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDistributedCache<UpdateCheckCacheItem> _cache;

        public UpdateCheckAppService(
            IJsonSerializer jsonSerializer,
            IHttpClientFactory httpClientFactory,
            IDistributedCache<UpdateCheckCacheItem> cache)
        {
            _jsonSerializer = jsonSerializer;
            _httpClientFactory = httpClientFactory;
            _cache = cache;
        }
        
        public virtual async Task<UpdateCheckOutput> CheckAsync()
        {
            return new()
            {
                CurrentVersion = GetType().Assembly.GetName().Version?.ToString(3),
                LatestVersion = await GetLatestVersionAsync()
            };
        }

        protected virtual async Task<string> GetLatestVersionAsync()
        {
            var item = await _cache.GetOrAddAsync("",
                async () => new UpdateCheckCacheItem
                    {LatestVersion = await RequestLatestVersionFromGithubRepositoryAsync()},
                () => new DistributedCacheEntryOptions
                    {AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CacheExpirationMinutes)});

            return item.LatestVersion;
        }

        protected virtual async Task<string> RequestLatestVersionFromGithubRepositoryAsync()
        {
            var client = _httpClientFactory.CreateClient();
            
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("product", "1"));

            var str = await client.GetStringAsync(LatestReleaseUri);
            var data = _jsonSerializer.Deserialize<JObject>(str);

            var tagName = data["tag_name"]?.ToString() ?? "";

            return tagName.Replace("v", "");
        }
    }
}