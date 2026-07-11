using Barachi.Data;
using Barachi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleDb")));

builder.Services.AddScoped<ILookupService, LookupService>();
builder.Services.AddScoped<IDeleteService, DeleteService>();
builder.Services.AddScoped<IKilldownService, KilldownService>();
builder.Services.AddScoped<IRetapingService, RetapingService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

// ถ้ายังไม่มีระบบ Login ไม่ต้องใช้ UseAuthorization()

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Scan}/{id?}");

app.Run();