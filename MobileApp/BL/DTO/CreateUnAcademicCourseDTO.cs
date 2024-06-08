﻿using System.ComponentModel.DataAnnotations;

namespace MobileApp.BL.DTO
{
    public class CreateUnAcademicCourseDTO
    {
   
        [Required(ErrorMessage = "اسم المادة مطلوب")]
        [MaxLength(50, ErrorMessage = "افصي طول للاسم 50 حرف")]
        [MinLength(3, ErrorMessage = "اقل طول للاسم 3 حروف")]
        public string Name { get; set; }
        [Required(ErrorMessage = "وصف المادة مطلوب")]
        [MaxLength(50, ErrorMessage = "افصي طول للوصف 50 حرف")]
        [MinLength(3, ErrorMessage = "اقل طول للوصف 3 حروف")]
        public string Description { get; set; }
      

        [Required(ErrorMessage = "صورة المادة مطلوبة")]
        public IFormFile Img { get; set; }
    }
}
