using Microsoft.EntityFrameworkCore;
using MobileApp.BL.DTO;
using MobileApp.BL.Interfaces;
using MobileApp.DAL.DataContext;
using MobileApp.DAL.Entities;
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
                Schedules = a.Schedules.Select(a => a)
            });

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


            var records = new List<Schedules>();
            var records2 = new List<CourseMaterialLinks>();
            var relateddata = db.schedules.Where(a => a.AcademicYearId == update.AcademicYearId && a.CourseId == update.CourseId && a.TeacherId==update.TeacherId).AsNoTracking().AsEnumerable();
            foreach (var item in relateddata)
            {
                records.Add(item);
            }
            var dataToDelete = db.AcademicYearCoursesTeachers.Where(a => a.AcademicYearId == update.AcademicYearId && a.CourseId == update.CourseId &&a.TeacherId==update.TeacherId ).FirstOrDefault();
            var CourseMaterialLinks = db.CourseMaterialLinks.Where(a => a.AcademicYearId == update.AcademicYearId && a.CourseId == update.CourseId && a.TeacherId == update.TeacherId).AsNoTracking().ToList();
            foreach (var item in CourseMaterialLinks)
            {
                records2.Add(item);
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
            db.schedules.AddRange(records);



            foreach (var item in records2)
            {
                item.CourseId = record.CourseId;
                item.AcademicYearId = record.AcademicYearId;
                item.TeacherId = record.TeacherId;
                item.Id = 0;

            }

            db.CourseMaterialLinks.AddRange(CourseMaterialLinks);
            db.SaveChanges();

        }
    }
}
