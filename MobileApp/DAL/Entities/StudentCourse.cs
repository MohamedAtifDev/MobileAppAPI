using System.ComponentModel.DataAnnotations.Schema;

namespace MobileApp.DAL.Entities
{
    public class StudentCourse
    {
        public int CourseId { get; set; }
        public int StudentId { get; set;}

        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set;}
        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set;}

        public DateTime AssignDate {  get; set; }   
    }
}
