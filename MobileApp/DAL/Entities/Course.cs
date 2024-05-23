using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileApp.DAL.Entities
{




    [Index("Name",IsUnique =true)]
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {  get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public IEnumerable<AcademicYearCourses> academicYearCourses { get; set; }

        public IEnumerable<StudentCourse> StudentCourses { get; set; }

       


    }
}
