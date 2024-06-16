using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileApp.DAL.Entities
{
    [Index("Name", IsUnique = true)]
    public class Group
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {  get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        public IEnumerable<CourseGroups> groups { get; set; }
        //public  double Price {  get; set; }
        //public int NumberOfStudents {  get; set; }

        public IEnumerable<StudentCourse> studentCourses { get; set; }

        //public IEnumerable<Schedules> schedules { get; set; }

    }
}
