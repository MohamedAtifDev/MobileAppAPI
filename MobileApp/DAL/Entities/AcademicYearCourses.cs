using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileApp.DAL.Entities
{

    [Index("YoutubeLink", IsUnique = true)]
    public class AcademicYearCourses
    {
        public int AcademicYearId {  get; set; }

        public int CourseId {  get; set; }

        [ForeignKey(nameof(AcademicYearId))]
        public AcademicYear AcademicYear { get; set; }
        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }


        public int TeacherId { get; set; }


        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }

        public string YoutubeLink { get; set; }
    }
}
