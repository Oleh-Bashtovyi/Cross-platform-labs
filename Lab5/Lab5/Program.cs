using Lab5.Services;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<Auth0UserService>();
builder.Services.AddHttpClient<ApiService>(client =>
{
    var apiAppUrl = builder.Environment.IsDevelopment() ? 
                builder.Configuration["ApiApp:BaseUrl"] : 
                builder.Configuration["ApiApp:SecureUrl"];

    if (apiAppUrl == null)
        throw new InvalidOperationException("Api app url must not be empty!");

    client.BaseAddress = new Uri(apiAppUrl);
});


builder.Services.AddAuthentication("AuthScheme")
    .AddCookie("AuthScheme", options =>
    {
        options.LoginPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
    });

builder.Services.AddAuthorization();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
