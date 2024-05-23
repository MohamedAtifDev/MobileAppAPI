using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MobileApp.DAL.Entities;

namespace MobileApp.DAL.DataContext
{
    public class DataContext:IdentityDbContext<AppUser>
        
    {

        public DataContext(DbContextOptions<DataContext> opt):base(opt)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AcademicYearCourses>().HasKey(a => new {a.CourseId,a.AcademicYearId});
            builder.Entity<StudentCourse>().HasKey(a => new { a.CourseId, a.StudentId });
            builder.Entity<IdentityRole>().HasData([
                new IdentityRole{
                    Id="1",
                    Name="User",
                    NormalizedName="User"
                    
                },  new IdentityRole{
                    Id="2",
                    Name="Admin",
                    NormalizedName="Admin"
                },
                  new IdentityRole{
                    Id="3",
                    Name="SuperAdmin",
                      NormalizedName="SuperAdmin"
                }
            ]);

        }

      
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        
        public DbSet<AcademicYear> AcademicYears { get; set; }

        public DbSet<Student> Students {  get; set; }

        public DbSet<Supervisor> Supervisors {  get; set; }

        public DbSet<AcademicYearCourses> AcademicYearCourses { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<UnAcademicCourse> unAcademicCourses { get; set; }
    }
}
