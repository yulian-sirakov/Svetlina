using Microsoft.EntityFrameworkCore;
using Svetlina.Data.Common;
using Svetlina.Data.Models;
using Svetlina.Services.Contracts;

namespace Svetlina.Services
{
    public class ScheduleContext : IDb<Schedule, int>
    {
        private readonly SvetlinaDbContext dbContext;

        public ScheduleContext(SvetlinaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(Schedule item)
        {
            try
            {
                //List<Project> projectsFromDb = new List<Project>();
                //foreach (Project project in item.Projects)
                //{
                //    Project projectfromDb = await dbContext.Projects.FindAsync(project.ProjectId);
                //    if (projectfromDb == null)
                //    {
                //        projectsFromDb.Add(project);
                //    }
                //    else
                //    {
                //        projectsFromDb.Add(projectfromDb);
                //    }
                //}
               // item.Projects = projectsFromDb;

                dbContext.Schedules.Add(item);
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
                var schedule = dbContext.Schedules.Find(key);
                if (schedule != null)
                {
                    dbContext.Schedules.Remove(schedule);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ICollection<Schedule>> ReadAllAsync(bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<Schedule> query = dbContext.Schedules;

                //if (useNavigationalProperties)
                //{
                //    query = query.Include(b => b.Projects);
                //}

                return await query.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Schedule> ReadAsync(int key, bool useNavigationalProperties = false)
        {
            try
            {
                //if (useNavigationalProperties)
                //{
                //    return dbContext.Schedules.Include(b => b.Projects).FirstOrDefault(b => b.ScheduleId == key);
                //}
                //else
              
                    return await dbContext.Schedules.FindAsync(key);
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateAsync(Schedule item, bool useNavigationalProperties = false)
        {
            try
            {
                Schedule scheduleFromDb = await ReadAsync(item.ScheduleId, useNavigationalProperties);
                scheduleFromDb.ScheduleName = item.ScheduleName;
                scheduleFromDb.StartDate = item.StartDate;
                scheduleFromDb.EndDate = item.EndDate;

                if (useNavigationalProperties)
                {

                    //if (item.Projects != null)
                    //{
                    //    var projectsFromDb = dbContext.Projects;
                    //    for (int i = 0; i < item.Projects.Count; i++)
                    //    {
                    //        var projectDb = projectsFromDb.FirstOrDefault(x => x.ProjectId == item.Projects[i].ProjectId);
                    //        if (projectDb != null)
                    //        {
                    //            item.Projects[i] = projectDb;
                    //        }
                    //    }
                    //}
                    //scheduleFromDb.Projects = item.Projects;
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


