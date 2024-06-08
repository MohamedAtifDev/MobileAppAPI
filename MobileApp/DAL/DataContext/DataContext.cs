using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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
        
            builder.Entity<AcademicYearCourses>().HasKey(a => new {a.AcademicYearId, a.CourseId });
            builder.Entity<AcademicYearCoursesTeachers>().HasKey(a => new { a.AcademicYearId, a.CourseId ,a.TeacherId});
            builder.Entity<AcademicYear>().HasOne(a=>a.user).WithOne(a=>a.AcademicYear).OnDelete(DeleteBehavior.SetNull);

            builder.Entity<CourseGroups>().HasKey(a => new { a.AcademicYearId, a.CourseId, a.TeacherId, a.GroupId });
            builder.Entity<UnAcademicCourse>().HasOne(a => a.Teacher).WithMany(a => a.unAcademicCourses).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<StudentCourse>().HasKey(a => new { a.CourseId, a.StudentId,a.AcademicYearId,a.TeacherId });
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

        //public DbSet<Student> Students {  get; set; }

        //public DbSet<Supervisor> Supervisors {  get; set; }
        public DbSet<Group> groups { get; set; }
        public DbSet<AcademicYearCourses> AcademicYearCourses { get; set; }
        public DbSet<AcademicYearCoursesTeachers> AcademicYearCoursesTeachers { get; set; }
        public DbSet<CourseMaterialLinks>CourseMaterialLinks { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<UnAcademicCourse> unAcademicCourses { get; set; }

        public DbSet<Schedules> schedules {  get; set; }

        public DbSet<CourseMaterialFiles> CourseMaterialFiles { get; set; }

        public DbSet<CourseGroups>CourseGroups { get; set; }
    }
}
