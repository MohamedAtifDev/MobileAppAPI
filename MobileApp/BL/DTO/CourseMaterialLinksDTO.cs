using MobileApp.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileApp.BL.DTO
{
    public class CourseMaterialLinksDTO
    {
        public int Id { get; set; }

        public string url { get; set; }


        public string Title { get; set; }

        public string Description { get; set; }
        public int AcademicYearId { get; set; }

        public int CourseId { get; set; }




        public int TeacherId { get; set; }


  
    }
}
