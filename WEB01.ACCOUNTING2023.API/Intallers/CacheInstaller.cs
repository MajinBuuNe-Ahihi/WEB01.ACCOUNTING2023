using WEB01.ACCOUNTING2023.CORE.Interfaces.Configurations;
using WEB01.ACCOUNTING2023.CORE.Interfaces.Installers;
using StackExchange.Redis;
using WEB01.ACCOUNTING2023.API.Services;

namespace WEB01.ACCOUNTING2023.API.Intallers
{
    public class CacheInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var redisConfiguration = new RedisConfiguration();
            configuration.GetSection("RedisConfiguration").Bind(redisConfiguration);
            services.AddSingleton(redisConfiguration);
            if(redisConfiguration.Enable == false)
            {
                return;
            }

            services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisConfiguration.ConnectionString));

            services.AddStackExchangeRedisCache(option => option.Configuration = redisConfiguration.ConnectionString);

            services.AddSingleton<IResponseCacheService, ResponseCacheService>();

        }
    }
}
