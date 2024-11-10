
using Lab5.Services;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<Auth0UserService>();
builder.Services.AddHttpClient<ApiService>(client =>
{
    var apiAppUrl = builder.Configuration["ApiApp:Url"] ?? throw new ArgumentException("Api app url is empty!");
    client.BaseAddress = new Uri(apiAppUrl);
});
//}).ConfigureHttpClient(async (serviceProvider, client) =>
//{
//    var auth0UserService = serviceProvider.GetRequiredService<Auth0UserService>();
//    var accessToken = await auth0UserService.GetAccessTokenAsync(); 
//    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
//});

builder.Services.AddAuthentication("AuthScheme")
    .AddCookie("AuthScheme", options =>
    {
        options.LoginPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
