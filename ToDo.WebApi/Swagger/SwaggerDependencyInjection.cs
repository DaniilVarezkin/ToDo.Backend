using Microsoft.OpenApi.Models;
using System.Reflection;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;
using Unchase.Swashbuckle.AspNetCore.Extensions.Options;

namespace ToDo.WebApi.Swagger
{
    public static class SwaggerDependencyInjection
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ToDo API",
                    Version = "1.0",
                    Description = "API для управления задачами: регистрация, JWT-аутентификация, CRUD и статистика.",
                    Contact = new OpenApiContact
                    {
                        Name = "Почта",
                        Email = "daniil.varezkin@gmail.com"
                    }
                });


                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);

                var xmlDomain = Path.Combine(AppContext.BaseDirectory, "ToDo.Domain.xml");
                config.IncludeXmlComments(xmlDomain);

                var xmlApplication = Path.Combine(AppContext.BaseDirectory, "ToDo.Application.xml");
                config.IncludeXmlComments(xmlApplication);


                // Описание схемы Bearer
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Введите JWT"
                });

                //Добавляем авторизацию только на методы с атрибутом [Authorize]
                config.OperationFilter<AuthorizeCheckOperationFilter>();

                config.OperationFilter<RequestBodyOperationFilter>();

                //Добавления описания к перечислениям
                config.AddEnumsWithValuesFixFilters(opt =>
                {
                    opt.IncludeXmlCommentsFrom(xmlDomain);
                    opt.DescriptionSource = DescriptionSources.XmlComments;
                    opt.IncludeDescriptions = true;
                });

            });
            return services;
        }
    }
}
