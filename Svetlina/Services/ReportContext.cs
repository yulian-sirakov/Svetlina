using Microsoft.EntityFrameworkCore;
using Svetlina.Data.Common;
using Svetlina.Data.Models;
using Svetlina.Services.Contracts;

namespace Svetlina.Services
{
    public class ReportContext : IDb<Report, int>
    {
        private readonly SvetlinaDbContext dbContext;

        public ReportContext(SvetlinaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task CreateAsync(Report item)
        {
            try
            {
                Customer customerfromDb = await dbContext.Users.FindAsync(item.CustomerId);
                if (customerfromDb != null)
                {
                    item.Customer = customerfromDb;
                }

                dbContext.Reports.Add(item);
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
                var report = dbContext.Reports.Find(key);
                if (report != null)
                {
                    dbContext.Reports.Remove(report);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ICollection<Report>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Report> query = dbContext.Reports;
                if (useNavigationalProperties)
                {
                    query = query.Include(b => b.Customer);
                }

                return await query.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Report> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                if (useNavigationalProperties)
                {
                    return dbContext.Reports.Include(b => b.Customer).FirstOrDefault(b => b.ReportId == key);
                }
                else
                {
                    return await dbContext.Reports.FindAsync(key);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateAsync(Report item, bool useNavigationalProperties = false)
        {
            try
            {
                Report reportsFromDb = await ReadAsync(item.ReportId, useNavigationalProperties);
                reportsFromDb.ReportName = item.ReportName;
                reportsFromDb.DateCreated = item.DateCreated;
                reportsFromDb.Content = item.Content;


                if (useNavigationalProperties)
                {
                    if (item.Customer != null)
                    {
                        Customer customerFromDb = await dbContext.Users.FindAsync(item.CustomerId);
                        if (customerFromDb != null)
                        {
                            item.Customer = customerFromDb;
                        }

                    }
                    reportsFromDb.Customer = item.Customer;

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



