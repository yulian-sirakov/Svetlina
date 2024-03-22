using Microsoft.EntityFrameworkCore;
using Svetlina.Data.Common;
using Svetlina.Data.Models;
using Svetlina.Services.Contracts;

namespace Svetlina.Services
{
    public class ProjectContext : IDb<Project, int>
    {
        private readonly SvetlinaDbContext dbContext;
        private readonly ScheduleContext scheduleContext;
        public ProjectContext(SvetlinaDbContext dbContext, ScheduleContext scheduleContext)
        {
            this.dbContext = dbContext;
            this.scheduleContext = scheduleContext;
        }

        public async Task CreateAsync(Project item)
        {
            try
            {
                List<Product> productsFromDb = new List<Product>();
                foreach (Product product in item.ProjectsProducts)
                {
                    Product productFromDb = await dbContext.Products.FindAsync(product.ProductId);
                    if (productFromDb == null)
                    {
                        productsFromDb.Add(product);
                    }
                    else
                    {
                        productsFromDb.Add(productFromDb);
                    }
                }
                item.ProjectsProducts = productsFromDb;

                List<Worker> workersFromDb = new List<Worker>();
                foreach (Worker worker in item.Workers)
                {
                    Worker workerFromDb = await dbContext.Workers.FindAsync(worker.WorkerId);
                    if (workerFromDb == null)
                    {
                        workersFromDb.Add(worker);
                    }
                    else
                    {
                        workersFromDb.Add(workerFromDb);
                    }
                }
                item.Workers = workersFromDb;


                Schedule scheduleFromDb = await dbContext.Schedules.FindAsync(item.Schedule.ScheduleId);
                if (scheduleFromDb != null)
                {
                    item.Schedule = scheduleFromDb;
                }
                else
                {
                    Schedule sch = new Schedule(item.Customer.UserName,item.StartDate,item.EndDate);
                    item.Schedule = sch;
                }


                Customer customerFromDb = await dbContext.Users.FindAsync(item.Customer.Id);
                if (customerFromDb != null)
                {
                    item.Customer = customerFromDb;
                }
                dbContext.Projects.Add(item);
                await dbContext.SaveChangesAsync  ();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteAsync(int key)
        {
            try
            {
                var project = dbContext.Projects.Find(key);
                if (project != null)
                {
                    dbContext.Projects.Remove(project);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ICollection<Project>> ReadAllAsync(bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<Project> query = dbContext.Projects;
                if (useNavigationalProperties)
                {
                    query = query.Include(b => b.Schedule).Include(p => p.ProjectsProducts).Include(c => c.Customer).Include(v => v.Workers);
                }

                return await query.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Project> ReadAsync(int key, bool useNavigationalProperties = false)
        {
            try
            {
                if (useNavigationalProperties)
                {
                    return await dbContext.Projects
                        .Include(p => p.Schedule)
                        .Include(p => p.Workers)
                        .Include(p => p.ProjectsProducts)
                        .Include(p => p.Customer)
                        .FirstOrDefaultAsync(b => b.ProjectId == key);
                }
                else
                {
                    return await dbContext.Projects.FindAsync(key);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateAsync(Project item, bool useNavigationalProperties = false)
        {
            try
            {
                Project projectFromDb = await ReadAsync(item.ProjectId, useNavigationalProperties);
                projectFromDb.ProjectName = item.ProjectName;
                projectFromDb.EndDate = item.EndDate;
                projectFromDb.StartDate = item.StartDate;
                projectFromDb.Description = item.Description;

                if (useNavigationalProperties)
                {


                    List<Product> productsFromDb = new List<Product>();
                    foreach (Product product in item.ProjectsProducts)
                    {
                        Product productFromDb = await dbContext.Products.FindAsync(product.ProductId);
                        if (productFromDb == null)
                        {
                            productsFromDb.Add(product);
                        }
                        else
                        {
                            productsFromDb.Add(productFromDb);
                        }
                    }
                    projectFromDb.ProjectsProducts = productsFromDb;

                    List<Worker> workersFromDb = new List<Worker>();
                    foreach (Worker worker in item.Workers)
                    {
                        Worker workerFromDb = await dbContext.Workers.FindAsync(worker.WorkerId);
                        if (workerFromDb == null)
                        {
                            workersFromDb.Add(worker);
                        }
                        else
                        {
                            workersFromDb.Add(workerFromDb);
                        }
                    }
                    projectFromDb.Workers = workersFromDb;

                    Schedule scheduleFromDb = await dbContext.Schedules.FindAsync(item.Schedule.ScheduleId);
                    if (scheduleFromDb != null)
                    {
                        projectFromDb.Schedule = scheduleFromDb;
                    }
                    else
                    {
                        projectFromDb.Schedule = item.Schedule;
                    }

                    Customer customerFromDb = await dbContext.Users.FindAsync(item.Customer.Id);
                    if (customerFromDb != null)
                    {
                        projectFromDb.Customer = customerFromDb;
                    }
                    else
                    {
                        projectFromDb.Customer = item.Customer;
                    }
                }
                await dbContext.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}


