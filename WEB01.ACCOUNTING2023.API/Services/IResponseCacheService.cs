namespace WEB01.ACCOUNTING2023.API.Services
{
    public interface IResponseCacheService
    {
        public Task SetCacheResponseServiceAsync(string key, object response, TimeSpan timeout);
        public Task<string> GetCacheResponseServiceAsync(string key);
    }
}
