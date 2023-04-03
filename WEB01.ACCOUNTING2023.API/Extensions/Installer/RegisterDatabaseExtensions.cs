using WEB01.ACCOUNTING2023.CORE.Configurations;
using WEB01.ACCOUNTING2023.CORE.Interfaces.Ifrastructures;
using WEB01.ACCOUNTING2023.INFRASTRUCTURE.Respository;

namespace WEB01.ACCOUNTING2023.API.Extensions.Installer
{
    public static class RegisterDatabaseExtensions
    {
        public static  void RegisterDatabase(this IServiceCollection services,IConfiguration configuration )
        {
            var databaseConfig = new DataBaseConfiguration();
            var value = configuration.GetSection("DataBaseConfiguration");
            databaseConfig.ConnectionString = value["ConnectionString"];
            services.AddSingleton(databaseConfig);
            IDatabase mySql = new MySQLRepository(databaseConfig);
            services.AddSingleton(mySql);
        }
    }
}
