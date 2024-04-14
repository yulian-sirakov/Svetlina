using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Svetlina.Data.Models;

namespace Svetlina.Data.Common
{
    public class SvetlinaDbContext : IdentityDbContext
    {
        public SvetlinaDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=YULIYAN\\SQLEXPRESS;Database=SvetlinaDbv2;Trusted_Connection=True;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<IdentityUser>(x=>x.HasKey(p=>p.Id));
            //modelBuilder.Entity<ProjectProduct>(entity
            //=> entity.HasKey(pp => new{pp.ProjectId, pp.ProductId}));

            //modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(t => new { t.LoginProvider, t.ProviderKey });
            //modelBuilder.Entity<IdentityUserRole<string>>().HasKey(t => new { t.UserId, t.RoleId });
            //modelBuilder.Entity<IdentityUserToken<string>>().HasKey(t => new { t.UserId });

            base.OnModelCreating(modelBuilder);
        }

        public new DbSet<Customer> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Svetlina.Data.Models.Cart> Carts { get; set; }
    }
}
