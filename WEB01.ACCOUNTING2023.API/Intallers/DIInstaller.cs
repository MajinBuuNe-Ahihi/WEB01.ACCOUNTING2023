using WEB01.ACCOUNTING2023.API.Extensions.Installer;
using WEB01.ACCOUNTING2023.CORE.Configurations;
using WEB01.ACCOUNTING2023.CORE.Entities.DTO;
using WEB01.ACCOUNTING2023.CORE.Entities.Models;
using WEB01.ACCOUNTING2023.CORE.Interfaces.Ifrastructures;
using WEB01.ACCOUNTING2023.CORE.Interfaces.Installers;
using WEB01.ACCOUNTING2023.CORE.Interfaces.Services;
using WEB01.ACCOUNTING2023.CORE.Services;
using WEB01.ACCOUNTING2023.INFRASTRUCTURE.Respository;

namespace WEB01.ACCOUNTING2023.API.Intallers
{
    public class DIInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDatabase(configuration);
            services.AddScoped(typeof(IIfrastructureBase<>), typeof(InfrastructureBase<>));
            services.AddScoped<IServicesBase, ServiceBase>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeServices, EmployeeServices>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IImportExportServices<EmployeesDTO>, ExcelServices<EmployeesDTO, EmployeeExcelDTO>>();
        }
    }
}
