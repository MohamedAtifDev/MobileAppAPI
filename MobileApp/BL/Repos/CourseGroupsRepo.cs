using Microsoft.EntityFrameworkCore;
using MobileApp.BL.DTO;
using MobileApp.BL.Interfaces;
using MobileApp.DAL.DataContext;
using MobileApp.DAL.Entities;
using System.Text.RegularExpressions;

namespace MobileApp.BL.Repos
{
    public class CourseGroupsRepo : ICourseGroup
    {
        private readonly DataContext db;

        public CourseGroupsRepo(DataContext db)
        {
            this.db = db;
        }
        public void Create(CourseGroups courseGroups)
        {
           db.CourseGroups.Add(courseGroups);
            db.SaveChanges();
        }

        public void Delete(CourseGroups courseGroups)
        {
            db.CourseGroups.Remove(db.CourseGroups.Where(a => a.TeacherId == courseGroups.TeacherId && a.CourseId == courseGroups.CourseId && a.AcademicYearId == courseGroups.AcademicYearId && a.GroupId == courseGroups.GroupId).FirstOrDefault());
            db.SaveChanges();        
        }


        public IEnumerable<CourseGroups> GetAll()
        {
            return db.CourseGroups.ToList();
        }

        public IEnumerable<GetGroupsDTO> GetGroups(int AcademicYear, int Course, int Teacher)
        {
            var data = db.CourseGroups.Where(a => a.TeacherId == Teacher && a.CourseId == Course && a.AcademicYearId == AcademicYear).Include(a => a.Group);

            return data.Select(a => new GetGroupsDTO
            {
               
               Id=a.Group.Id,
               Name=a.Group.Name,
               NumberOfStudents=a.NumberOfStudents,
               Price=a.Price


            });
        }

public bool isExist(int AcademicYear, int Course, int Teacher,int Group)
        {
             return db.CourseGroups.Where(a => a.TeacherId == Teacher && a.CourseId == Course && a.AcademicYearId == AcademicYear && a.GroupId == Group)
            .FirstOrDefault() == null ? false : true;
        }


        public void update(UpdateCourseGroupDTO courseGroups)
        {
            db.ChangeTracker.Clear();
            var subdata = new List<Schedules>();
            var oldData = db.CourseGroups.Where(a => a.TeacherId == courseGroups.TeacherId && a.CourseId==courseGroups.CourseId && a.AcademicYearId == courseGroups.AcademicYearId && a.GroupId == courseGroups.GroupId).FirstOrDefault();

            var Schedules=db.schedules.Where(a => a.TeacherId == courseGroups.TeacherId && a.CourseId == courseGroups.CourseId && a.AcademicYearId == courseGroups.AcademicYearId && a.GroupID == courseGroups.GroupId);

            foreach (var schedule in Schedules)
            {
                subdata.Add(schedule);
            }
                var dataToAdd = new CourseGroups
            {
                CourseId = courseGroups.CourseId,
                GroupId = (int)courseGroups.NewGroupId,
                AcademicYearId = courseGroups.AcademicYearId,
                Price = courseGroups.Price,
                NumberOfStudents = courseGroups.NumberOfStudents,
           
                TeacherId = courseGroups.TeacherId,
            };

          
            db.CourseGroups.Remove(oldData);
            db.SaveChanges();
           db.CourseGroups.Add(dataToAdd);
            db.SaveChanges();

            foreach (var item in subdata)
            {
                item.Id = 0;
                item.CourseId = courseGroups.CourseId;
                item.GroupID = (int)courseGroups.NewGroupId;
                item.AcademicYearId = courseGroups.AcademicYearId;
            

           
              item.TeacherId = courseGroups.TeacherId;
            }

            db.schedules.AddRange(subdata);
            db.SaveChanges();
        }
    }
}
