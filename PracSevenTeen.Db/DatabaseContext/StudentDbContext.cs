using Microsoft.EntityFrameworkCore;
using PracSevenTeen.Db.DataSeed;
using PracSevenTeen.Models.Models;

namespace PracSevenTeen.Db.DatabaseContext
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasKey(e => new { e.UserId, e.RoleId });

            modelBuilder.SeedRole();
            modelBuilder.SeedAdmin();
            modelBuilder.SeedUserRole();
        }

    }
}
