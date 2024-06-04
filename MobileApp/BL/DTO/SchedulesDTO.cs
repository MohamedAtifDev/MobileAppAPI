using System.ComponentModel.DataAnnotations;

namespace MobileApp.BL.DTO
{
    public class SchedulesDTO
    {
        public int? Id { get; set; }

        public string? Day { get; set; }


        [DataType(DataType.Time)]
        public TimeOnly? Time { get; set; }


        public int? AcademicYearId { get; set; }

        public int? CourseId { get; set; }




        public int? TeacherId { get; set; }
    }
}
