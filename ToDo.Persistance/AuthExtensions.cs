using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToDo.Persistance.Identity;
using ToDo.Persistance.Services;

namespace ToDo.Persistance
{
    public static class AuthExtensions
    {
        
        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            //Добавляем аутентификацию по Jwt
            services
                .AddIdentity<ApplicationUser, IdentityRole>(opts =>
                {
                    // требования к паролям
                    opts.Password.RequireDigit = false;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequiredLength = 6;

                    // можно включить подтверждение email перед логином
                    opts.SignIn.RequireConfirmedEmail = false;

                    // ограничения на имена пользователей, lockout и т. д.
                    opts.User.RequireUniqueEmail = true;
                    opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    opts.Lockout.MaxFailedAccessAttempts = 5;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();


            services.AddScoped<JwtService>();
            services.Configure<AuthSettings>(
                configuration.GetSection("AuthSettings"));


            var authSettings = configuration.GetSection(nameof(AuthSettings))
                .Get<AuthSettings>();

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
