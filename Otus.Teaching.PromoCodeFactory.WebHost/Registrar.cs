using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Otus.Teaching.PromoCodeFactory.DataAccess.DataBaseContext;

namespace Otus.Teaching.PromoCodeFactory.WebHost
{
    public static class Registrar
    {

        public static IServiceCollection AddCustomServicesExtensionMethod(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqLiteDB(configuration);
            return services;
        }

        private static IServiceCollection AddSqLiteDB(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connectionString));
            return services;
        }
    }
}
