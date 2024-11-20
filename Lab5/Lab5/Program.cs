using Lab6.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<Auth0UserService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<ApiService>(client =>
{
    var apiAppUrl = builder.Configuration["ApiApp:BaseUrl"];

    if (apiAppUrl == null)
    {
        throw new InvalidOperationException("Api web app url must not be empty!");
    }

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
