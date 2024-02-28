using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Svetlina.Data.Common;
using Svetlina.Data.Models;

namespace Svetlina.Services
{
    public class CustomerContext
    {
        private readonly SvetlinaDbContext context;
        private readonly UserManager<Customer> userManager;
        public CustomerContext(SvetlinaDbContext dbContext, UserManager<Customer> userManager)
        {

            context = dbContext;
            this.userManager = userManager;

        }
        public async Task SeedDataAsync(string adminPass, string adminEmail)
        {
            //await context.Database.MigrateAsync();

            int userRoles = await context.UserRoles.CountAsync();

            if (userRoles == 0)
            {
                await ConfigureAdminAccountAsync(adminPass, adminEmail);
            }
        }

        public async Task ConfigureAdminAccountAsync(string password, string email)
        {
            Customer adminIdentityUser = await context.Users.FirstAsync();

            if (adminIdentityUser != null)
            {
                await userManager.AddToRoleAsync(adminIdentityUser, Role.Administrator.ToString());
                await userManager.AddPasswordAsync(adminIdentityUser, password);
                await userManager.SetEmailAsync(adminIdentityUser, email);
            }
        }

        public async Task<Tuple<IdentityResult, Customer>> CreateAsync(string username, string password, string email, string Phone, Role role)
        {
            try
            {
                Customer user = new Customer(username, email, Phone);
                IdentityResult result = await userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    return new Tuple<IdentityResult, Customer>(result, user);
                }

                if (role == Role.Administrator)
                {
                    await userManager.AddToRoleAsync(user, Role.Administrator.ToString());
                }
                else
                {
                    await userManager.AddToRoleAsync(user, Role.User.ToString());
                }
                return new Tuple<IdentityResult, Customer>(IdentityResult.Success, user);
            }
            catch (Exception ex)
            {
                IdentityResult result = IdentityResult.Failed(new IdentityError() { Code = "Registration", Description = ex.Message });
                return new Tuple<IdentityResult, Customer>(result, null);
            }
        }

        public async Task<Customer> LogInAsync(string username, string password)
        {
            try
            {
                Customer user = await userManager.FindByNameAsync(username);

                if (user == null)
                {
                    return null;
                }

                IdentityResult result = await userManager.PasswordValidators[0].ValidateAsync(userManager, user, password);

                if (result.Succeeded)
                {
                    return await context.Users.FindAsync(user.Id);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Customer> ReadAsync(string key, bool useNavigationalProperties = false)
        {
            try
            {
                if (!useNavigationalProperties)
                {
                    // If you want to use the API
                    return await userManager.FindByIdAsync(key);
                }

                return await context.Users.Include(u => u.Projects).Include(u => u.Reports).SingleOrDefaultAsync(u => u.Id == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Customer>> ReadAllAsync(bool useNavigationalProperties = false)
        {
            try
            {

                if (!useNavigationalProperties)
                {
                    // If you want to use the API
                    return await context.Users.ToListAsync();
                }

                return await context.Users.Include(u => u.Projects).Include(u => u.Reports).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateAsync(string id, string username, string phone)
        {
            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    Customer user = await context.Users.FindAsync(id);
                    user.UserName = username;
                    user.PhoneNumber = phone;
                    await userManager.UpdateAsync(user);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteAsync(string username)
        {
            try
            {
                Customer user = await FindAsync(username);

                if (user == null)
                {
                    throw new InvalidOperationException("User not found for deletion!");
                }

                await userManager.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Customer> FindAsync(string username)
        {
            try
            {
                // Identity return Null if there is no user!
                return await userManager.FindByNameAsync(username);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}

