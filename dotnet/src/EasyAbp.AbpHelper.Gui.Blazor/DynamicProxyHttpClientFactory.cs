using System;
using System.Net.Http;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.DynamicProxying;

namespace EasyAbp.AbpHelper.Gui.Blazor
{
    [Dependency(ReplaceServices = true)]
    public class DynamicProxyHttpClientFactory : IDynamicProxyHttpClientFactory, ITransientDependency
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DynamicProxyHttpClientFactory(IHttpClientFactory httpClientFactory) 
        {
            _httpClientFactory = httpClientFactory;
        }
        
        public HttpClient Create()
        {
            var client = _httpClientFactory.CreateClient();

            client.Timeout = TimeSpan.FromMinutes(10);
            
            return client;
        }

        public HttpClient Create(string name)
        {
            var client = _httpClientFactory.CreateClient(name);
            
            client.Timeout = TimeSpan.FromMinutes(10);

            return client;
        }
    }
}