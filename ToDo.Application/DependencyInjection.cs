using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using FluentValidation;
using ToDo.Application.Common.Behaviors;

namespace ToDo.Application
{
    /// <summary>
    /// Статический класс для регистрации сервисов слоя Application в контейнер зависимостей.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Добавляет сервисы MediatR, валидации и поведения конвейера (PipelineBehavior) для слоя Application.
        /// </summary>
        /// <param name="services">Коллекция сервисов для настройки DI.</param>
        /// <returns>Тот же <see cref="IServiceCollection"/>, расширенный новыми сервисами.</returns>
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            // Регистрация MediatR: сканирование текущей сборки на запросы и обработчики
            services.AddMediatR(config =>
                config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            // Регистрация валидаторов FluentValidation из текущей сборки
            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });

            // Добавление поведения конвейера для валидации запросов
            services.AddTransient(
                typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
