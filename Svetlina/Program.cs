using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Svetlina.Data.Common;
using Svetlina.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<SvetlinaDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<SvetlinaDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ProductContext, ProductContext>();
builder.Services.AddScoped<ProjectContext, ProjectContext>();
builder.Services.AddScoped<ReportContext, ReportContext>();
builder.Services.AddScoped<ScheduleContext, ScheduleContext>();
builder.Services.AddScoped<WorkerContext, WorkerContext>();
builder.Services.AddScoped<CustomerContext, CustomerContext>();


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
