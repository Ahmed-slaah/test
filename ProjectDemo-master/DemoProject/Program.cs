using DemoProject.Core.IHelper;
using DemoProject.EF.Helper;
using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.EF;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromDays(1);//You can set Time   
});
builder.Services.AddMvc();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews();
var dbConnextionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(dbConnextionString));

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
//builder.Services.AddTransient<ISessionService, SessionService>();

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
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
