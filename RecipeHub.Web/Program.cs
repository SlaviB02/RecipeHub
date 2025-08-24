using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecipeHub.Data;
using RecipeHub.Data.Repository.Interfaces;
using RecipeHub.Data.Repository;
using RecipeHub.Data.Models;
using RecipeHub.Services.Data.Interfaces;
using RecipeHub.Services.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<RecipeHubDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<RecipeHubDbContext>();
builder.Services.AddScoped<IRepository<Recipe>,Repository<Recipe>>();
builder.Services.AddScoped<IRepository<Ingredient>, Repository<Ingredient>>();
builder.Services.AddScoped<IRepository<Category>, Repository<Category>>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
