using DapperWithSQL;
using DapperWithSQL.DataContext;
using DapperWithSQL.IRepository;
using DapperWithSQL.Repository;
using DapperWithSQL.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddOpenApiDocument();

//builder.Services.AddControllersWithViews();

builder.Services.AddScoped<DapperContext>();
builder.Services.AddScoped<IOTPService,OTPService>();
builder.Services.AddScoped<IRegistrationRepository,RegistrationRepository>();
builder.Services.AddScoped<IImageRepository,ImageRepository>();

//default scheme is cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie();

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

app.UseOpenApi();
app.UseSwaggerUi(options =>
{
    options.Path = "/api";
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
