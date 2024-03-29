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

        public async Task<ICollection<Worker>> ReadAllAsync(bool useNavigationalProperties = false)
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

        public async Task<Worker> ReadAsync(int key, bool useNavigationalProperties = false)
        {
            try
            {
                
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
                workerFromDb.WorkerImage = item.WorkerImage;
                workerFromDb.WorkerInfo= item.WorkerInfo;
                workerFromDb.SpecialisationType=item.SpecialisationType;
              
                await dbContext.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

