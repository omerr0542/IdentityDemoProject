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
    config.User.RequireUniqueEmail = true; // Ayný mail adresiyle birden fazla kullanýcý kaydolamasýn
    //config.Password.RequiredLength = 6; // Minimum password uzunluðu
    //config.Password.RequireNonAlphanumeric = false; // Alfanümerik karakter zorunluluðu
    //config.Password.RequireUppercase = false; // Büyük harf zorunluluðu
    //config.Password.RequireLowercase = false; // Küçük harf zorunluluðu
    //config.Password.RequireDigit = false; // Rakam zorunluluðu
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
