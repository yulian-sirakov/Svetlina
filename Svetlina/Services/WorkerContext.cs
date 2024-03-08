using Microsoft.EntityFrameworkCore;
using Svetlina.Data.Common;
using Svetlina.Data.Models;
using Svetlina.Services.Contracts;

namespace Svetlina.Services
{

    public class WorkerContext : IDb<Worker, int>
    {
        private readonly SvetlinaDbContext dbContext;
        public WorkerContext(SvetlinaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(Worker item)
        {
            try
            {
                //Project projectFromDb = await dbContext.Projects.FindAsync(item.ProjectId);
                //if (projectFromDb != null)
                //{
                //    item.Project = projectFromDb;
                //}

                dbContext.Workers.Add(item);
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
                var worker = dbContext.Workers.Find(key);
                if (worker != null)
                {
                    dbContext.Workers.Remove(worker);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ICollection<Worker>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Worker> query = dbContext.Workers;

                return await  query.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<Worker> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                //if (useNavigationalProperties)
                //{
                //    return dbContext.Workers.Include(b => b.Project).FirstOrDefault(b => b.ProjectId == key);
                //}
                //else
                //{
                return await dbContext.Workers.FindAsync(key);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateAsync(Worker item, bool useNavigationalProperties = false)
        {
            try
            {
                Worker workerFromDb = await ReadAsync(item.WorkerId, useNavigationalProperties);
                workerFromDb.WorkerName = item.WorkerName;
                workerFromDb.PhoneNumber = item.PhoneNumber;
                //workerFromDb.SpecialisationType = item.SpecialisationType;


                //if (useNavigationalProperties)
                //{
                //    if (item.Project != null)
                //    {
                //        var projectFromDb = await dbContext.Projects.FindAsync(item.ProjectId);
                //        if (projectFromDb != null)
                //        {
                //            item.Project = projectFromDb;
                //        }

                //    }
                //    workerFromDb.Project = item.Project;


                //}
                await dbContext.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

