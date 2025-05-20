using System.Reflection;
using ToDo.Application;
using ToDo.Application.Common.Mapping;
using ToDo.Persistance;
using ToDo.WebApi.Middleware;

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

var app = builder.Build();

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

//Swagger

app.Run();
