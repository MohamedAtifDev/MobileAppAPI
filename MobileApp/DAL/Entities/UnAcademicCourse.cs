using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MobileApp.DAL.Entities
{

    [Index("Name",IsUnique =true)]
    public class UnAcademicCourse
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public int? TeacherId {  get; set; }
        public string ImgName {  get; set; }
        public Teacher? Teacher {  get; set; }

    }
}
