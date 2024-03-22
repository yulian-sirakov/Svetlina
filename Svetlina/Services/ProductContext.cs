using Microsoft.EntityFrameworkCore;
using Svetlina.Data.Common;
using Svetlina.Data.Models;
using Svetlina.Services.Contracts;

namespace Svetlina.Services
{
    public class ProductContext : IDb<Product, int>
    {
        private readonly SvetlinaDbContext dbContext;

        public ProductContext(SvetlinaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(Product item)
        {
            try
            {
                List<Project> projectsFromDb = new List<Project>();
                foreach (Project project in item.ProjectsProducts)
                {
                    Project projectfromDb = await dbContext.Projects.FindAsync(project.ProjectId);
                    if (projectfromDb == null)
                    {
                        projectsFromDb.Add(project);
                    }
                    else
                    {
                        projectsFromDb.Add(projectfromDb);
                    }
                }
                item.ProjectsProducts = projectsFromDb;

                dbContext.Products.Add(item);
                await dbContext.SaveChangesAsync();
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
                var product = dbContext.Products.Find(key);
                if (product != null)
                {
                    dbContext.Products.Remove(product);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ICollection<Product>> ReadAllAsync(bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<Product> query = dbContext.Products;
                if (useNavigationalProperties)
                {
                    query = query.Include(b => b.ProjectsProducts);
                }

                return await query.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Product> ReadAsync(int key, bool useNavigationalProperties = false)
        {
            try
            {
                if (useNavigationalProperties)
                {
                    return dbContext.Products.Include(b => b.ProjectsProducts).FirstOrDefault(b => b.ProductId == key);
                }
                else
                {
                    return await dbContext.Products.FindAsync(key);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateAsync(Product item, bool useNavigationalProperties = false)
        {
            try
            {
                Product productFromDb = await ReadAsync(item.ProductId, useNavigationalProperties);
                productFromDb.ProductName = item.ProductName;
                productFromDb.ProductId = item.ProductId;
                productFromDb.Price = item.Price;
                productFromDb.ProductImage= item.ProductImage;


                if (useNavigationalProperties)
                {


                    List<Project> projectsFromDb = new List<Project>();
                    foreach (Project project in item.ProjectsProducts)
                    {
                        Project projectfromDb = await dbContext.Projects.FindAsync(project.ProjectId);
                        if (projectfromDb == null)
                        {
                            projectsFromDb.Add(project);
                        }
                        else
                        {
                            projectsFromDb.Add(projectfromDb);
                        }
                    }
                    productFromDb.ProjectsProducts = projectsFromDb;


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

