using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;
using ToDo.Application;
using ToDo.Application.Common.Mapping;
using ToDo.Persistance;
using ToDo.WebApi.Middleware;
using ToDo.WebApi.Swagger;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;
using Unchase.Swashbuckle.AspNetCore.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IMapped).Assembly));
});

builder.Services.AddApplication();
builder.Services.AddPersistance(builder.Configuration);


builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddSwaggerServices();

var app = builder.Build();

//Инициализация БД
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<AppDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        throw ex;
    }
}

app.UseCustomExceptionHandler();
app.UseHttpsRedirection();
app.UseRouting();

//Добавить CORS

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(cfg =>
{
    cfg.RoutePrefix = string.Empty;
    cfg.SwaggerEndpoint("swagger/v1/swagger.json", "ToDo API");
});

app.Run();
