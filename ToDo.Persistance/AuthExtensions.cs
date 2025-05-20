using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToDo.Persistance.Identity;
using ToDo.Persistance.Services;

namespace ToDo.Persistance
{
    /// <summary>
    /// Расширения для настройки JWT-аутентификации и Identity.
    /// </summary>
    public static class AuthExtensions
    {
        /// <summary>
        /// Добавляет JWT-аутентификацию, настройку Identity и сервис JwtService.
        /// </summary>
        /// <param name="services">Коллекция сервисов для настройки DI.</param>
        /// <param name="configuration">Конфигурация приложения для чтения настроек аутентификации.</param>
        /// <returns>Тот же <see cref="IServiceCollection"/>, расширенный сервисами аутентификации.</returns>
        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Настройка ASP.NET Core Identity с параметрами пароля и блокировки
            services
                .AddIdentity<ApplicationUser, IdentityRole>(opts =>
                {
                    // Настройки сложности пароля
                    opts.Password.RequireDigit = false;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequiredLength = 6;

                    // Подтверждение email перед входом
                    opts.SignIn.RequireConfirmedEmail = false;

                    // Требование уникального email и параметры блокировки
                    opts.User.RequireUniqueEmail = true;
                    opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    opts.Lockout.MaxFailedAccessAttempts = 5;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // Регистрация JwtService для генерации токенов
            services.AddScoped<JwtService>();

            // Чтение секции настроек AuthSettings из конфигурации
            services.Configure<AuthSettings>(
                configuration.GetSection("AuthSettings"));

            var authSettings = configuration
                .GetSection(nameof(AuthSettings))
                .Get<AuthSettings>();

            // Настройка схем аутентификации по JWT Bearer
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = authSettings.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(authSettings.SecretKey))
                };
            });

            return services;
        }
    }
}