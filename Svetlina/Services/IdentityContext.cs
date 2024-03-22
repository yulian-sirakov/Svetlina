//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Svetlina.Data.Common;
using Svetlina.Data.Models;

namespace Svetlina.Services
{
    public class IdentityContext
    {
        UserManager<Customer> userManager;
        SvetlinaDbContext context;

        public IdentityContext(SvetlinaDbContext context, UserManager<Customer> userManager)
        {
            this.userManager = userManager;
            this.context = context;
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





        //CRUD

        public async Task<Tuple<IdentityResult, Customer>> CreateUserAsync(string username, string password, string email, string phonenumber, Role role)
        {
            try
            {
                Customer user = new Customer(username, email, phonenumber);
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

        public async Task<Customer> LogInUserAsync(string username, string password)
        {
            try
            {
                Customer user = await userManager.FindByNameAsync(username);

                //Customer user = await userManager.FindByEmailAsync(username);

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

        public async Task<Customer> ReadAsync(string key, bool useNavigationalProperties)
        {
            try
            {
                if (useNavigationalProperties)
                {
                    return await context.Users.Include(x => x.Reports).Include(x => x.Projects).SingleOrDefaultAsync(x => x.Id == key);
                }
                return await userManager.FindByIdAsync(key);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Customer>> ReadAllUsersAsync(bool useNavigationalProperties)
        {
            try
            {
                if (useNavigationalProperties)
                {
                    return await context.Users.Include(x => x.Reports).Include(x => x.Projects).ToListAsync();
                }
                return await context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateUserAsync(string id, string username)
        {
            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    Customer user = await context.Users.FindAsync(id);
                    user.UserName = username;
                    await userManager.UpdateAsync(user);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteUserByNameAsync(string name)
        {
            try
            {
                Customer user = await FindUserByNameAsync(name);

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

        public async Task<Customer> FindUserByNameAsync(string name)
        {
            try
            {
                // Identity return Null if there is no user!
                return await userManager.FindByNameAsync(name);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
