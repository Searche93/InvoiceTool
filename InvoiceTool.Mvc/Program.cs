using InvoiceTool.Application;
using InvoiceTool.Domain;
using InvoiceTool.Infrastructure;
using InvoiceTool.Infrastructure.Persistence;
using InvoiceTool.Mvc.Data;
using InvoiceTool.Mvc.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found."); ;

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AppDbContext>();

// Add config
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();
builder.Services.AddSingleton<IWebHostEnvironment>(builder.Environment);

builder.Services
    .AddDomain()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();


var userName = builder.Configuration["Auth:UserName"];
var password = builder.Configuration["Auth:Password"];
using var scope = app.Services.CreateScope();

if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    if (await userManager.FindByEmailAsync(userName) == null)
    {
        var user = new ApplicationUser
        {
            Email = userName,
            UserName = userName
        };

        await userManager.CreateAsync(user, password);
    }
}

var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
var addDemoData = config.GetValue<bool>("Settings:AddDemoData");

if (addDemoData)
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    DemoDataSeeder.Seed(db);
}

app.Run();
