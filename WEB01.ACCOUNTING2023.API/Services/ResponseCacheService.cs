using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WEB01.ACCOUNTING2023.API.Services
{

    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public ResponseCacheService(IDistributedCache distributedCache,IConnectionMultiplexer connectionMultiplexer)
        {
            _distributedCache = distributedCache;
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task<string> GetCacheResponseServiceAsync(string key)
        {
            var cacheResponse = await _distributedCache.GetStringAsync(key);
            return string.IsNullOrEmpty(cacheResponse) ? null : cacheResponse;

        }

        public async Task SetCacheResponseServiceAsync(string key, object response, TimeSpan timeout)
        {
           if(response == null)
            {
                return;
            }

            var serializerResponse = JsonConvert.SerializeObject(response, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            await _distributedCache.SetStringAsync(key, serializerResponse,
                new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = timeout
                }) ;

        }
    }
}
