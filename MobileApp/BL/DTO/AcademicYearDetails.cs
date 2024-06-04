using MobileApp.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileApp.BL.DTO
{
    public class AcademicYearDetails
    {

        public int AcademicYearId { get; set; }




        public string AcademicYearName { get; set; }


        public IEnumerable<CourseDTo> Courses { get; set; }
        public int ?TeacherId { get; set; }

    

        public string Teachers { get; set; }

      }
}
