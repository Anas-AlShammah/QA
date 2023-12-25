using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QA.Data;
using QA.Interfaces;
using QA.Models;
using QA.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services
.AddDbContext<AppDbContext>
(opions => opions
.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 -._@+";
})
   .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IQuestion,QuestionService>();
builder.Services.AddScoped<IUserService, IdentityUserService>();
builder.Services.AddScoped<ICategory, CategoryService>();

builder.Services.AddAuthorization(options =>
{
    // Add "Name of Policy", and the Lambda returns a definition
    options.AddPolicy("create", policy => policy.RequireClaim("permissions", "create"));
    options.AddPolicy("update", policy => policy.RequireClaim("permissions", "update"));
    options.AddPolicy("delete", policy => policy.RequireClaim("permissions", "delete"));
});
var app = builder.Build();
//UpdateDatabase(app.Services);
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=login}/{id?}");

app.Run();
//static void UpdateDatabase(IServiceProvider services)
//{
//    using (var serviceScope = services.CreateScope())
//    {
//        using (var db = serviceScope.ServiceProvider.GetService<AppDbContext>())
//        {
//            db.Database.Migrate();
//        }
//    }
//}