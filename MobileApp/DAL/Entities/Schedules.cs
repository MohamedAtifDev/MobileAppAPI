using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileApp.DAL.Entities
{
    public class Schedules
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        public string Day {  get; set; }


        
        public String Time { get; set; }


        public int AcademicYearId { get; set; }

        public int CourseId { get; set; }




        public int TeacherId { get; set; }

        public int GroupID { get; set; }
        [ForeignKey("AcademicYearId,CourseId,TeacherId,GroupID")]


        public CourseGroups CourseGroups { get; set; }


    



    }
}
