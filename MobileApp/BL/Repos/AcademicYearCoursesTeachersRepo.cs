using Microsoft.EntityFrameworkCore;
using MobileApp.BL.DTO;
using MobileApp.BL.Interfaces;
using MobileApp.DAL.DataContext;
using MobileApp.DAL.Entities;
using Org.BouncyCastle.Tls;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MobileApp.BL.Repos
{
    public class AcademicYearCoursesTeachersRepo : IAcademicYeatCoursesTeachers
    {
        private readonly DataContext db;

        public AcademicYearCoursesTeachersRepo(DataContext db)
        {
          
       this.db = db;
        }
        public void Add(AcademicYearCoursesTeachers academicYearCoursesTeachers)
        {
            db.AcademicYearCoursesTeachers.Add(academicYearCoursesTeachers);
            db.SaveChanges();
        }

        public void Delete(int AcademicYearID, int CourseId, int TeacherId)
        {
            db.AcademicYearCoursesTeachers.Remove(db.AcademicYearCoursesTeachers.Where(a => a.AcademicYearId == AcademicYearID && a.CourseId == CourseId && a.TeacherId == TeacherId).FirstOrDefault());

            db.SaveChanges();
        }

        public IEnumerable<AcademicYearCoursesTeachers> GetAll()
        {
           return db.AcademicYearCoursesTeachers.ToList();
        }

        public AcademicYearCoursesTeachers GetById(int AcademicYearID, int CourseId, int TeacherID)
        {
            return db.AcademicYearCoursesTeachers.Where(a=>a.AcademicYearId==AcademicYearID && a.CourseId==CourseId &&a.TeacherId==TeacherID).FirstOrDefault();
        }

        public IEnumerable<TeacherDTO> GetTeachers(int AcademicYearID, int CourseId)
        {
            var data= db.AcademicYearCoursesTeachers.Where(a => a.AcademicYearId == AcademicYearID && a.CourseId == CourseId).Include(a => a.Teacher);
            var res = data.Select(a => new TeacherDTO
            {
                Id = a.Teacher.Id,
                Name = a.Teacher.Name,
                Email = a.Teacher.Email,
                NumberOfLessons = a.NumberOfLessons,
                ZoomLink = a.Teacher.ZoomLink,

                Groups = a.groups.Select(a => new FroupWithScheduleDTO
                {GroupName=a.Group.Name,
                Price=a.Price,
                NumberOFStudents=a.NumberOfStudents,
                schedules=a.Schedules.Select(a=>new ScheduleDTO
                {
                    Day=a.Day,
                    Time=a.Time
                })

                })
            }); ;

            return res;
        }



        public void Update(UpdateDTO update)
        {
            var record = new AcademicYearCoursesTeachers
            {
                AcademicYearId = update.AcademicYearId,
                CourseId = update.CourseId,
                TeacherId=(int)update.NewTeacherId,
                endDate=update.endDate,
                startDate=update.startDate,
                YoutubeLink=update.YoutubeLink,
                NumberOfLessons=update.NumberOfLessons
            };
            //db.ChangeTracker.Clear();

            //var data = GetById(update.AcademicYearId, update.CourseId, update.TeacherId);
            //data.startDate = update.startDate;
            //data.TeacherId = (int)update.NewTeacherId;
            //data.YoutubeLink = update.YoutubeLink;
            //data.endDate = update.endDate;
            //data.NumberOfLessons = update.NumberOfLessons;

            //db.SaveChanges();

            var records = new List<Schedules>();
            var records2 = new List<CourseMaterialLinks>();

            var records3 = new List<CourseMaterialFiles>();

            var records4 = new List<CourseGroups>();

            var records5 = new List<StudentCourse>();
            var relateddata = db.schedules.Where(a => a.AcademicYearId == update.AcademicYearId && a.CourseId == update.CourseId && a.TeacherId == update.TeacherId).AsNoTracking().AsEnumerable();

            var dataToDelete = db.AcademicYearCoursesTeachers.Where(a => a.AcademicYearId == update.AcademicYearId && a.CourseId == update.CourseId && a.TeacherId == update.TeacherId).FirstOrDefault();
            var CourseMaterialLinks = db.CourseMaterialLinks.Where(a => a.AcademicYearId == update.AcademicYearId && a.CourseId == update.CourseId && a.TeacherId == update.TeacherId).AsNoTracking().ToList();

            var courseMaterialFile = db.CourseMaterialFiles.Where(a => a.AcademicYearId == update.AcademicYearId && a.CourseId == update.CourseId && a.TeacherId == update.TeacherId).AsNoTracking().ToList();



            var courseGroups = db.CourseGroups.Where(a => a.AcademicYearId == update.AcademicYearId && a.CourseId == update.CourseId && a.TeacherId == update.TeacherId).AsNoTracking().ToList();


            var studentCourse = db.StudentCourses.Where(a => a.AcademicYearId == update.AcademicYearId && a.CourseId == update.CourseId && a.TeacherId == update.TeacherId).AsNoTracking().ToList();


            foreach (var item in relateddata)
            {
                records.Add(item);
            }
            foreach (var item in CourseMaterialLinks)
            {
                records2.Add(item);
            }


            foreach (var item in courseMaterialFile)
            {
                records3.Add(item);
            }


            foreach (var item in studentCourse)
            {
                records5.Add(item);
            }

            foreach (var item in courseGroups)
            {

                records4.Add(item);
            }
            db.AcademicYearCoursesTeachers.Remove(dataToDelete);
            db.AcademicYearCoursesTeachers.Add(record);
            db.SaveChanges();


            foreach (var item in records)
            {
                item.CourseId = record.CourseId;
                item.AcademicYearId = record.AcademicYearId;
                item.TeacherId = record.TeacherId;
                item.Id = 0;

            }
           



            foreach (var item in records2)
            {
                item.CourseId = record.CourseId;
                item.AcademicYearId = record.AcademicYearId;
                item.TeacherId = record.TeacherId;
                item.Id = 0;

            }



            db.CourseMaterialLinks.AddRange(records2);



            foreach (var item in records3)
            {
                item.CourseId = record.CourseId;
                item.AcademicYearId = record.AcademicYearId;
                item.TeacherId = record.TeacherId;
                item.Id = 0;

            }

            db.CourseMaterialFiles.AddRange(records3);


            foreach (var item in records4)
            {
                item.CourseId = record.CourseId;
                item.AcademicYearId = record.AcademicYearId;
                item.TeacherId = record.TeacherId;


            }

      

            foreach (var item in records5)
            {
                item.CourseId = record.CourseId;
                item.AcademicYearId = record.AcademicYearId;
                item.TeacherId = record.TeacherId;


            }

        
            db.StudentCourses.AddRange(records5);
            db.SaveChanges();

            db.CourseGroups.AddRange(records4);
              db.SaveChanges();

            db.schedules.AddRange(records);

            db.SaveChanges();
        }
    }
}
