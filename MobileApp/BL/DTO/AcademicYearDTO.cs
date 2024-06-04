﻿using MobileApp.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MobileApp.BL.DTO
{
    public class AcademicYearDTO
    {
        [Required(ErrorMessage = "ID is Required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم المستخدم مطلوب ")]
        [MaxLength(50, ErrorMessage = "افصي طول للاسم 50 حرف")]
        [MinLength(3, ErrorMessage = "اقل طول للاسم 3 حروف")]
        public string? Name { get; set; }
        public string? AdminID { get; set; }

    }
}
