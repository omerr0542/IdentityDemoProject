using EmailApp.Context;
using EmailApp.Entites;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});

builder.Services.AddIdentity<AppUser, AppRole>(config =>
{
    config.User.RequireUniqueEmail = true; // Ayn� mail adresiyle birden fazla kullan�c� kaydolamas�n
    //config.Password.RequiredLength = 6; // Minimum password uzunlu�u
    //config.Password.RequireNonAlphanumeric = false; // Alfan�merik karakter zorunlulu�u
    //config.Password.RequireUppercase = false; // B�y�k harf zorunlulu�u
    //config.Password.RequireLowercase = false; // K���k harf zorunlulu�u
    //config.Password.RequireDigit = false; // Rakam zorunlulu�u
})
                 .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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


app.Run();
