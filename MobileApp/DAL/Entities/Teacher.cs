﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileApp.DAL.Entities
{


    [Index("Email",IsUnique =true)]
    public class Teacher
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(11)]
        public string Phone { get; set; }
        [StringLength(50)]
        public string Address { get; set; }


        public string Email { get; set; }

        public string ZoomLink {  get; set; }

        public string ImgName { get; set; }
        public IEnumerable<AcademicYearCoursesTeachers>? academicYearCoursesTeachers { get; set; }

        public IEnumerable<UnAcademicCourse> ?unAcademicCourses { get; set; }

    }
}
