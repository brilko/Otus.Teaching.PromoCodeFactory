using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.DataAccess.DataBaseContext;
using Otus.Teaching.PromoCodeFactory.DataAccess.Repositories;

namespace Otus.Teaching.PromoCodeFactory.WebHost
{
    public static class Registrar
    {

        public static IServiceCollection AddCustomServicesExtensionMethod(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.InstallSqLiteDB(configuration);
            services.InstallRepositories();
            return services;
        }

        private static IServiceCollection InstallSqLiteDB(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connectionString));
            return services;
        }

        private static IServiceCollection InstallRepositories(this IServiceCollection services)
        {            
            services.AddTransient<IRepository<Employee>, EfRepository<Employee>>();
            services.AddTransient<IRepository<Role>, EfRepository<Role>>();
            return services;
        }
    }
}
