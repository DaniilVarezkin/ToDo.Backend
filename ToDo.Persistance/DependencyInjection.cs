using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Application.Interfaces;


namespace ToDo.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            var connectionString = configuration["DbConnectionString"];

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            services.AddScoped<IAppDbContext>(provider => 
                provider.GetRequiredService<AppDbContext>()
            );

            return services;
        }
    }
}
