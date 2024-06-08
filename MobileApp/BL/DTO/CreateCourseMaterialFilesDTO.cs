﻿using System.ComponentModel.DataAnnotations;

namespace MobileApp.BL.DTO
{
    public class CreateCourseMaterialFilesDTO
    {
        [Required(ErrorMessage = "الملف مطلوب")]
        public IFormFile File { get; set; }

        [Required(ErrorMessage = "عنوان الفديو مطلوب")]
        public string Title { get; set; }
        [Required(ErrorMessage = "وصف الفديو مطلوب")]
        public string Description { get; set; }
        [Required(ErrorMessage = "الصف الدراسي مطلوب")]
        public int AcademicYearId { get; set; }

        [Required(ErrorMessage = "المادة مطلوب")]
        public int CourseId { get; set; }



        [Required(ErrorMessage = "المعلم مطلوب")]
        public int TeacherId { get; set; }

        
        
    }
}
