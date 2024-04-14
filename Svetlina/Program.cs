using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Svetlina.Data.Common;
using Svetlina.Data.Models;
using Svetlina.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<SvetlinaDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<Customer>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<SvetlinaDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped<ProductContext, ProductContext>();
builder.Services.AddScoped<ProjectContext, ProjectContext>();
builder.Services.AddScoped<ReportContext, ReportContext>();
builder.Services.AddScoped<ScheduleContext, ScheduleContext>();
builder.Services.AddScoped<WorkerContext, WorkerContext>();
builder.Services.AddScoped<CustomerContext, CustomerContext>();
builder.Services.AddScoped<IdentityContext, IdentityContext>();
builder.Services.AddScoped<CartContext, CartContext>();



builder.Services.AddIdentity<Customer, IdentityRole>(io =>
{
    io.Password.RequiredLength = 5;
    io.Password.RequireNonAlphanumeric = false;
    io.Password.RequiredUniqueChars = 0;
    io.Password.RequireUppercase = false;
    io.Password.RequireDigit = false;

    io.User.RequireUniqueEmail = false;

    io.SignIn.RequireConfirmedEmail = false;

    io.Lockout.MaxFailedAccessAttempts = 3;
}).AddEntityFrameworkStores<SvetlinaDbContext>()
                .AddDefaultTokenProviders();


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
