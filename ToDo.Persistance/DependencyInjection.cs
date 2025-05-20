using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Application.Interfaces;


namespace ToDo.Persistance
{
    /// <summary>
    /// Статический класс для регистрации сервисов слоя Persistence в контейнер зависимостей.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Добавляет в DI-контейнер DbContext и сопутствующие сервисы для работы с базой данных.
        /// </summary>
        /// <param name="services">Коллекция сервисов для настройки DI.</param>
        /// <param name="configuration">Конфигурация приложения для получения строки подключения.</param>
        /// <returns>Тот же <see cref="IServiceCollection"/>, расширенный сервисами Persistence.</returns>
        public static IServiceCollection AddPersistance(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Чтение строки подключения из конфигурации
            var connectionString = configuration["DbConnectionString"];

            // Регистрация контекста базы данных с использованием SQLite
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            // Регистрация абстракции IAppDbContext для использования в слоях Application и Domain
            services.AddScoped<IAppDbContext>(provider =>
                provider.GetRequiredService<AppDbContext>()
            );

            return services;
        }
    }
}
