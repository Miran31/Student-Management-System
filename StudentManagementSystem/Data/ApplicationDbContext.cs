using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Areas.Admin.Models;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<StudentClass> studentClasses { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Configure the relationship between ApplicationUser and StudentClass
        //    modelBuilder.Entity<ApplicationUser>()
        //        .HasOne(au => au.StudentClass)
        //        .WithMany()
        //        .HasForeignKey(au => au.StudentClassId);
        //}
    }
}
