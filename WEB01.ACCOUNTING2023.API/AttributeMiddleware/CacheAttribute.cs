using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using WEB01.ACCOUNTING2023.API.Services;
using WEB01.ACCOUNTING2023.CORE.Interfaces.Configurations;

namespace WEB01.ACCOUNTING2023.API.AttributeMiddleware
{
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveSeconds;
        public CacheAttribute(int timeToLiveSeconds = 1000)
        {
            _timeToLiveSeconds = timeToLiveSeconds;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheConfiguration = context.HttpContext.RequestServices.GetRequiredService<RedisConfiguration>();
            if(!cacheConfiguration.Enable)
            {
                await next();
                return;
            }
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            var cacheKey = GenCacheKey(context.HttpContext.Request);
            var cacheResponse = await cacheService.GetCacheResponseServiceAsync(cacheKey);

            if(!string.IsNullOrEmpty(cacheResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contentResult;
                return;
            }
            var excutedContext = await next();
            if(excutedContext.Result is ObjectResult objectResult)
            {
                await cacheService.SetCacheResponseServiceAsync(cacheKey, objectResult.Value, TimeSpan.FromSeconds(_timeToLiveSeconds));
            }
            {

            }
        }

        private  string GenCacheKey(HttpRequest req)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"{req.Path}");
            foreach(var (key,value) in req.Query.OrderBy(x=> x.Key)) {
                keyBuilder.Append($"{key} --{value}");
            }
            return keyBuilder.ToString();

        }
    }
}
