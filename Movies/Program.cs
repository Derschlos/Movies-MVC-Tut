using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Movies.Data;
using Movies.Models;
using Movies.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MoviesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesContext") ?? throw new InvalidOperationException("Connection string 'MoviesContext' not found.")));
builder.Services.AddDbContext<MoviesLoginContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesLoginContextConnection") ?? throw new InvalidOperationException("Connection string 'MoviesContext' not found.")));

builder.Services.AddDefaultIdentity<MoviesUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<MoviesLoginContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

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
app.UseAuthentication();;

app.UseAuthorization();
app.MapRazorPages();
//app.MapControllerRoute(
//    name: "MovieModels",
//    pattern: "/Movies/{action}/{id?}",
//    defaults: new { controller = "MovieModelsController"});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
