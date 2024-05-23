using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileApp.DAL.Entities
{

    [Index("Email",IsUnique =true)]
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(11)]
        public string Phone {  get; set; }

        public string Email { get; set; }
        public IEnumerable<StudentCourse> StudentCourses { get; set; }


    }
}
