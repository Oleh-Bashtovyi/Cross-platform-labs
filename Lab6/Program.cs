using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Lab6.Data;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json"));
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API V1", Version = "v1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "My API V2", Version = "v2" });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var databaseType = builder.Configuration["DatabaseType"]?.ToLower();

    switch (databaseType)
    {
        case "mssql":
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"));
            break;
        case "postgres":
            options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection"));
            break;
        case "sqlite":
            options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"));
            break;
        case "inmemory":
            options.UseInMemoryDatabase("InMemoryDb");
            break;
        default:
            throw new InvalidOperationException($"Specify correct database provider: mssql, postgres, sqlite or InMemory. " +
                                                $"Current: {databaseType}");
    }
});


builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0); // Default to v1
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});



// Налаштування автентифікації Auth0
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";
        options.Audience = builder.Configuration["Auth0:Audience"];

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = $"https://{builder.Configuration["Auth0:Domain"]}",
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Auth0:Audience"],
            ValidateLifetime = true
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWebApp",
        policy => policy
            .WithOrigins(builder.Configuration["WebApp:Url"] ?? throw new ArgumentException("Web app url is empty!"))
            .AllowAnyMethod()
            .AllowAnyHeader());
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    var dbType = app.Configuration["DatabaseType"]?.ToLower();

    // Виконуємо міграції тільки для реляційних БД (для inmemory не працює)
    if (dbType is "mssql" or "postgres" or "sqlite")
    {
        context.Database.Migrate();
    }

    context.SeedData();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowWebApp");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
