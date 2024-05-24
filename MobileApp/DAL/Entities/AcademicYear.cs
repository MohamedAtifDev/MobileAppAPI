using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileApp.DAL.Entities
{
    [Index("Name",IsUnique =true)]
    public class AcademicYear
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id {  get; set; }

        [StringLength(50)]
        public string Name {  get; set; }

       
   public IEnumerable<AcademicYearCourses> AcademicYearCourses { get; set; }

        public string AdminID {  get; set; }

        [ForeignKey("AdminID")]
        public AppUser user { get; set; }

    }
}
