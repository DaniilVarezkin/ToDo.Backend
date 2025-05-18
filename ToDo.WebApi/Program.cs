using System.Reflection;
using ToDo.Application;
using ToDo.Application.Common.Mapping;
using ToDo.Persistance;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IMapped).Assembly));
});

builder.Services.AddApplication();
builder.Services.AddPersistance(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<AppDbContext>();
        DbInitializer.Initialize(context);
    }
    catch(Exception ex)
    {
        throw ex;
    }
}

app.UseHttpsRedirection();
app.UseRouting();

//�������� CORS

app.UseStaticFiles();

//CustomExceptionHandler

app.MapControllers();

//Swagger

app.Run();
