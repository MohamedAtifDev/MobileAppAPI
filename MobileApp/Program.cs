
using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using MobileApp.BL.AutoMapper;
using MobileApp.BL.Interfaces;
using MobileApp.BL.Repos;
using MobileApp.DAL.DataContext;
using MobileApp.DAL.Entities;
using Newtonsoft;
using System.Text.Json;

namespace MobileApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddDbContextPool<DataContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("MobileApp")).UseExceptionProcessor();
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {

                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;


            }).AddDefaultTokenProviders().AddEntityFrameworkStores<DataContext>();

            // Add services to the container.

            builder.Services.AddAutoMapper(opt =>
            {
                opt.AddProfile(new MyProfile());
            });

            builder.Services.AddTransient<IPasswordValidator<AppUser>, CustomPasswordValidator<AppUser>>();

          
         
            builder.Services.AddScoped<ITeacher, TeacherRepo>();
            builder.Services.AddScoped<IStudentCourse, StudentCoursesRepo>();
            builder.Services.AddScoped<IAcademicYearCourses, AcademicYearCoursesRepo>();
            builder.Services.AddScoped<IAcademicYear, AcademicYearRepo>();
            builder.Services.AddScoped<ICourse, CourseRepo>();
            builder.Services.AddScoped<IGroup, GroupRepo>();
            builder.Services.AddScoped<IStudentCourse, StudentCoursesRepo>();
            builder.Services.AddScoped<IAcademicYearCourses, AcademicYearCoursesRepo>();
            builder.Services.AddScoped<IAcademicYearCourses, AcademicYearCoursesRepo>();
            builder.Services.AddScoped<IAcademicYeatCoursesTeachers, AcademicYearCoursesTeachersRepo>();
            builder.Services.AddScoped<ICourseMaterialLinks, CourseMaterialLinksRepo>();
            builder.Services.AddScoped<ICourseMaterialFiles, CourseMaterialFilesRepo>();
            builder.Services.AddScoped<IUnAcademicCourse, UnAcademicCourseRepo>();
            builder.Services.AddScoped<ISchedules, ScheduleRepo>();
            builder.Services.AddMvc()
        .ConfigureApiBehaviorOptions(options => {
            options.SuppressModelStateInvalidFilter = true;
        }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            builder.Services.AddControllers().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                opt.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
               
              
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(option =>
            {
                option.AddDefaultPolicy(opt =>
                {
                    opt.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Files")),
                RequestPath = "/Resources"

            }
            ); ;

            app.MapControllers();

            app.Run();
        }
    }
}
