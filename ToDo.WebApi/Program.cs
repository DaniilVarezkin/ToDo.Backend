using System.Reflection;
using ToDo.Application;
using ToDo.Application.Common.Mapping;
using ToDo.Application.Common.Mapping.MappingProfiles;
using ToDo.Persistance;
using ToDo.WebApi.Middleware;
using ToDo.WebApi.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IMapped).Assembly));
    config.AddProfile(new TaskItemMappingProfile());
});

builder.Services.AddApplication();
builder.Services.AddPersistance(builder.Configuration);


builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddSwaggerServices();

builder.Services.AddCors(config =>
{
    config.AddPolicy("default", options =>
    {
        options.AllowAnyHeader();
        options.AllowAnyMethod();
        options.AllowAnyOrigin();
    });
});

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

app.UseCors("default");

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
