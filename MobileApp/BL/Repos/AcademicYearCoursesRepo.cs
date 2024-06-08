using Microsoft.EntityFrameworkCore;
using MobileApp.BL.DTO;
using MobileApp.BL.Interfaces;
using MobileApp.DAL.DataContext;
using MobileApp.DAL.Entities;

namespace MobileApp.BL.Repos
{
    public class AcademicYearCoursesRepo : IAcademicYearCourses
    {
        private readonly DataContext db;

        public AcademicYearCoursesRepo(DataContext db)
        {
            this.db = db;
        }
        public void Add(AcademicYearCourses AcademicYearCourses)
        {
            this.db.AcademicYearCourses.Add(AcademicYearCourses);
            db.SaveChanges();
        }

        public int Count()
        {
            return db.AcademicYearCourses.Count
                ();
        }

        public void Delete(int AcademicYear, int CourseId)
        {
          var data= db.AcademicYearCourses.Where(a => a.CourseId == CourseId && a.AcademicYearId == AcademicYear).FirstOrDefault();

            db.AcademicYearCourses.Remove(data);
        }

        public IEnumerable<AcademicYearCourses> GetAll()
        {
            return db.AcademicYearCourses.AsNoTracking().ToList();
        }


        public IEnumerable<Course> GetCourses(int academicyear)
        {
            return db.AcademicYearCourses.Where(a=>a.AcademicYearId == academicyear).Include(a=>a.Course).Select(a=>new Course
            {
                Id = a.CourseId,
                Name=a.Course.Name
            });


        }

        public AcademicYearCourses GetById(int AcademicYear, int CourseId)
        {
            return db.AcademicYearCourses.Where(a => a.CourseId == CourseId && a.AcademicYearId == AcademicYear).FirstOrDefault();
        }

        public void Update(UpdateAcademicYearCoursesDTO update)
        {
            var record = new AcademicYearCourses
            {
                AcademicYearId = update.AcademicYearId,
                CourseId =(int) update.New_CourseId
            };


            var records = new List<AcademicYearCoursesTeachers>();

            var records2 = new List<CourseMaterialLinks>();

            var records3 = new List<CourseMaterialFiles>();

            var record4 = new List<Schedules>();

            var record5 = new List<StudentCourse>();

            var record6 = new List<CourseGroups>();    

            var courseMaterialFile = db.CourseMaterialFiles.Where(a => a.AcademicYearId == update.AcademicYearId && a.CourseId == update.Old_CourseId).AsNoTracking().ToList();

            var relateddata = db.AcademicYearCoursesTeachers.Where(a=>a.AcademicYearId == update.AcademicYearId && a.CourseId == update.Old_CourseId).AsNoTracking().AsEnumerable() ;

            var dataToDelete = db.AcademicYearCourses.Where(a => a.AcademicYearId == update.AcademicYearId && a.CourseId == update.Old_CourseId).FirstOrDefault();

            var CourseMaterialLinks = db.CourseMaterialLinks.Where(a => a.AcademicYearId == update.AcademicYearId && a.CourseId == update.Old_CourseId).AsNoTracking().ToList();

            var Studentcourse = db.StudentCourses.Where(a => a.AcademicYearId == update.AcademicYearId && a.CourseId == update.Old_CourseId).AsNoTracking().ToList();

           var CourseGroups=db.CourseGroups.Where(a => a.AcademicYearId == update.AcademicYearId && a.CourseId == update.Old_CourseId).AsNoTracking().ToList();

            var schedules = db.schedules.Where(a => a.AcademicYearId == update.AcademicYearId && a.CourseId == update.Old_CourseId).ToList();



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


            foreach (var item in schedules)
            {
                record4.Add(item);
            }

            foreach (var item in Studentcourse)
            {
                record5.Add(item);
            }



            foreach (var item in CourseGroups)
            {
                record6.Add(item);
            }




            db.AcademicYearCourses.Remove(dataToDelete);
            db.AcademicYearCourses.Add(record);
            db.SaveChanges();

     
            foreach (var item in records)
            {
                item.CourseId =record.CourseId;
                item.AcademicYearId = record.AcademicYearId;
             
            }
            db.AcademicYearCoursesTeachers.AddRange(records);



            foreach (var item in records2)
            {
                item.CourseId = record.CourseId;
                item.AcademicYearId = record.AcademicYearId;
                item.Id = 0;

            }

            db.CourseMaterialLinks.AddRange(records2);

            foreach (var item in records3)
            {
                item.CourseId = record.CourseId;
                item.AcademicYearId = record.AcademicYearId;
             
                item.Id = 0;

            }

            db.CourseMaterialFiles.AddRange(records3);


            foreach (var item in record6)
            {
                item.CourseId = record.CourseId;
                    item.AcademicYearId=record.AcademicYearId;
            }

            db.CourseGroups.AddRange(record6);

            foreach (var item in record4)
            {
                item.CourseId = record.CourseId;
                item.AcademicYearId = record.AcademicYearId;

                item.Id = 0;

            }
            db.schedules.AddRange(record4);


            foreach (var item in record5)
            {
                item.CourseId = record.CourseId;
                item.AcademicYearId = record.AcademicYearId;

               

            }
            db.StudentCourses.AddRange(record5);

            db.SaveChanges();
          


        }

       
    }
}
