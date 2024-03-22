using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Svetlina.Data.Common;
using Svetlina.Data.Models;
using Svetlina.Services;
using System.Data;

try
{
    IdentityOptions options = new IdentityOptions();
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 5;

    DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
    builder.UseSqlServer(
        "Server=YULIYAN\\SQLEXPRESS;Database=SvetlinaDbv2;Trusted_Connection=True;TrustServerCertificate=True"
        );

    SvetlinaDbContext dbContext = new SvetlinaDbContext(builder.Options);
    UserManager<Customer> userManager = new UserManager<Customer>(
        new UserStore<Customer>(dbContext), Options.Create(options),
        new PasswordHasher<Customer>(), new List<IUserValidator<Customer>>() { new UserValidator<Customer>() },
        new List<IPasswordValidator<Customer>>() { new PasswordValidator<Customer>() }, new UpperInvariantLookupNormalizer(),
        new IdentityErrorDescriber(), new ServiceCollection().BuildServiceProvider(),
        new Logger<UserManager<Customer>>(new LoggerFactory())
        );

    CustomerContext identityContext = new CustomerContext(dbContext, userManager);

    dbContext.Roles.Add(new IdentityRole("Administrator") { NormalizedName = "ADMINISTRATOR" });
    dbContext.Roles.Add(new IdentityRole("User") { NormalizedName = "USER" });
    await dbContext.SaveChangesAsync();

    Tuple<IdentityResult,Customer > result = await identityContext.CreateUserAsync("admin", "Admin123_", "admin@abv.bg", "0876897654", Role.Administrator);

    Console.WriteLine("Roles added successfully!");

    if (result.Item1.Succeeded)
    {
        Console.WriteLine("Admin account added successfully!");
    }
    else
    {
        Console.WriteLine("Admin account failed to be created!");
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{

}