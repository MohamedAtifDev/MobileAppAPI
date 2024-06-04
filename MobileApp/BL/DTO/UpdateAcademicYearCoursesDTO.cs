using System.ComponentModel.DataAnnotations;

namespace MobileApp.BL.DTO
{
    public class UpdateAcademicYearCoursesDTO
    {
   
        public int Old_AcademicYearId { get; set; }

        public int New_AcademicYearId { get; set; }



        public int Old_CourseId { get; set; }

        public int New_CourseId { get; set; }
    }
}
