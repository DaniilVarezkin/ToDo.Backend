using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using ToDo.Application;
using ToDo.Application.Common.Mapping;
using ToDo.Persistance;
using ToDo.Persistance.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IMapped).Assembly));
});

builder.Services.AddApplication();
builder.Services.AddPersistance(builder.Configuration);


//ƒобавл€ем аутентификацию по Jwt
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(opts =>
    {
        // требовани€ к парол€м
        opts.Password.RequireDigit = false;
        opts.Password.RequireNonAlphanumeric = false;
        opts.Password.RequiredLength = 6;

        // можно включить подтверждение email перед логином
        opts.SignIn.RequireConfirmedEmail = false;

        // ограничени€ на имена пользователей, lockout и т. д.
        opts.User.RequireUniqueEmail = true;
        opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        opts.Lockout.MaxFailedAccessAttempts = 5;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

builder.Services
  .AddAuthentication(options =>
  {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  })
  .AddJwtBearer(options =>
  {
      options.RequireHttpsMetadata = true;
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = jwtIssuer,
          ValidAudience = jwtAudience,
          IssuerSigningKey =
          new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
      };
  });

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
    catch(Exception ex)
    {
        throw ex;
    }
}

app.UseHttpsRedirection();
app.UseRouting();

//ƒобавить CORS

app.UseStaticFiles();

//CustomExceptionHandler


app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

//Swagger

app.Run();
