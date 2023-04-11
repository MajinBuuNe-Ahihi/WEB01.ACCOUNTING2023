using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB01.ACCOUNTING2023.CORE.Interfaces.Installers;

namespace WEB01.ACCOUNTING2023.API.Extensions.Installer
{
    public static class InstallerExtensions
    {
        /// <summary>
        /// đăng kí service
        /// </summary>
        /// <param name="services">service</param>
        /// <param name="configuration">config</param>
        public static void InstallerServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
           
            var installer = typeof(Program).Assembly.ExportedTypes.Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();
            installer.ForEach(installer => installer.InstallServices(services, configuration));
       }
    }
}
